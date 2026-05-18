using Microsoft.Extensions.Options;
using RestaurantApp.Domain;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Domain.Entities;
using RestaurantApp.Models;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text.Encodings.Web;

namespace RestaurantApp.Services;

public class RestaurantService
{
    private const string CLIENT_CONTROLLER = "Client";
    private const string KITCHEN_CONTROLLER = "Kitchen";
    private const string FRONTDESK_CONTROLLER = "FrontDesk";

    private HttpClient _httpClient;
    private MenuDto _menu = new MenuDto();

    public RestaurantService(IOptions<ApplicationSettings> options)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(options.Value.BaseAPIUrl);
        ClientOrder = new OrderDto();
    }

    public OrderDto ClientOrder { get; set; }

    public async Task<List<FoodDto>> GetFoodItemsAsync(bool forceReload = false)
    {
        if (!_menu.Foods.Any() || forceReload)
        {
            await LoadMenu();
        }

        return _menu.Foods.ToList();
    }

    public async Task<List<DrinkDto>> GetDrinkItemsAsync(bool forceReload = false)
    {
        if (!_menu.Drinks.Any() || forceReload)
        {
            await LoadMenu();
        }

        return _menu.Drinks.ToList();
    }

    public void SetFoodItemCount(int foodId, int count)
    {
        FoodDto foodDto = ClientOrder.Foods.First(x => x.Id == foodId);

        if (count < 1)
        {
            ClientOrder.Foods.Remove(foodDto);
        }
        else
        {
            foodDto.Count = count;
        }
    }

    public void SetDrinkItemCount(int drinkItem, int count)
    {
        DrinkDto drinkDto = ClientOrder.Drinks.First(x => x.Id == drinkItem);

        if (count < 1)
        {
            ClientOrder.Drinks.Remove(drinkDto);
        }
        else
        {
            drinkDto.Count = count;
        }
    }

    public async Task<bool> PlaceOrder()
    {
        DateTime now = DateTime.Now;
        ClientOrder.OrderName = $"{now.Hour}{now.Minute}{now.Second}";

        bool result = await GetResponseFromApi<bool>(HttpVerb.Post, CLIENT_CONTROLLER, ApiRoutes.Order, queryParams: null, content: ClientOrder);

        if (result == true)
        {
            ClientOrder = new OrderDto();

            await LoadMenu();
        }

        return result;
    }

    public async Task<bool> UpdateFood(FoodDto foodDto)
    {
        bool result = await GetResponseFromApi<bool>(HttpVerb.Put, FRONTDESK_CONTROLLER, ApiRoutes.Food, queryParams: null, content: foodDto);

        return result;
    }

    public async Task<bool> AddFood(FoodDto foodDto)
    {
        bool result = await GetResponseFromApi<bool>(HttpVerb.Post, FRONTDESK_CONTROLLER, ApiRoutes.Food, queryParams: null, content: foodDto);

        return result;
    }

    public async Task<bool> UpdateDrink(DrinkDto drinkDto)
    {
        bool result = await GetResponseFromApi<bool>(HttpVerb.Put, FRONTDESK_CONTROLLER, ApiRoutes.Drink, queryParams: null, content: drinkDto);

        return result;
    }

    public async Task<bool> AddDrink(DrinkDto drinkDto)
    {
        bool result = await GetResponseFromApi<bool>(HttpVerb.Post, FRONTDESK_CONTROLLER, ApiRoutes.Drink, queryParams: null, content: drinkDto);

        return result;
    }

    public async Task<bool> AnyNewOrders(int lastOrderId)
    {
        bool result = await GetResponseFromApi<bool>(HttpVerb.Get, KITCHEN_CONTROLLER, ApiRoutes.AnyOrders, [("lastOrderId", lastOrderId)], null);

        return result;
    }

    public async Task<List<OrderDto>> GetNewOrdersForKitchen(int lastOrderId)
    {
        List<OrderDto>? result = await GetResponseFromApi<List<OrderDto>>(HttpVerb.Get, KITCHEN_CONTROLLER, ApiRoutes.Orders, queryParams: [("lastOrderId", lastOrderId)], content: null);

        return result ?? new();
    }

    public async Task<List<OrderDto>> GetNewOrdersForFrontDesk(int lastOrderId)
    {
        List<OrderDto>? result = await GetResponseFromApi<List<OrderDto>>(HttpVerb.Get, FRONTDESK_CONTROLLER, ApiRoutes.Orders, queryParams: [("lastOrderId", lastOrderId)], content: null);

        return result ?? new();
    }

    public async Task<bool> MarkOrderAsServed(int orderId)
    {
        bool result = await GetResponseFromApi<bool>(HttpVerb.Put, KITCHEN_CONTROLLER, ApiRoutes.OrderServed, queryParams: null, content: orderId);

        return result;
    }

    public async Task<bool> MarkOrderAsPaid(int orderId)
    {
        bool result = await GetResponseFromApi<bool>(HttpVerb.Put, FRONTDESK_CONTROLLER, ApiRoutes.OrderPaid, queryParams: null, content: orderId);

        return result;
    }

    public async Task<List<FoodTypeDto>> GetFoodTypes()
    {
        List<FoodTypeDto>? result = await GetResponseFromApi<List<FoodTypeDto>>(HttpVerb.Get, FRONTDESK_CONTROLLER, ApiRoutes.FoodType, queryParams: null, content: null);

        return result ?? new();
    }

    public async Task<List<DrinkTypeDto>> GetDrinkTypes()
    {
        List<DrinkTypeDto>? result = await GetResponseFromApi<List<DrinkTypeDto>>(HttpVerb.Get, FRONTDESK_CONTROLLER, ApiRoutes.DrinkType, queryParams: null, content: null);

        return result ?? new();
    }

    private async Task LoadMenu()
    {
        _menu = await GetResponseFromApi<MenuDto>(HttpVerb.Get, CLIENT_CONTROLLER, ApiRoutes.Menu, queryParams: null, content: null) ?? new MenuDto();
    }

    private async Task<T?> GetResponseFromApi<T>(HttpVerb httpVerb,
        string controller,
        string targetEntity,
        IEnumerable<(string, object)>? queryParams = null,
        object? content = null)
    {
        try
        {
            string uri = $"{controller}/{targetEntity}";

            if (queryParams is not null)
            {
                string query = string.Join("&",
                    queryParams.Select(p => $"{p.Item1}={Uri.EscapeDataString(p.Item2?.ToString() ?? string.Empty)}"));

                uri += "?" + query;
            }

            HttpResponseMessage? response = await DoAPICallWithRetry(httpVerb, uri, content);

            if (response is null)
            {
                return default;
            }
            
            if (typeof(T) == typeof(bool) && response.IsSuccessStatusCode && string.IsNullOrWhiteSpace(await response.Content.ReadAsStringAsync()))
            {
                return (T)(object)true;
            }

            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);

            return default;
        }
    }

    private async Task<HttpResponseMessage?> DoAPICallWithRetry(HttpVerb httpVerb, string uri, object? content = null)
    {
        HttpResponseMessage? response = null;

        for (int i = 0; i < 3; i++)
        {
            try
            {
                response = httpVerb switch
                {
                    HttpVerb.Get => await _httpClient.GetAsync(uri),
                    HttpVerb.Post => await _httpClient.PostAsync(uri, JsonContent.Create(content)),
                    HttpVerb.Put => await _httpClient.PutAsync(uri, JsonContent.Create(content)),
                    HttpVerb.Delete => await _httpClient.DeleteAsync(uri),
                    _ => throw new ArgumentOutOfRangeException(nameof(httpVerb))
                };

                response.EnsureSuccessStatusCode();

                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"API call not successful. StatusCode: {response?.StatusCode}; Msg: {ex.Message}");

                if (i == 2) throw;
                await Task.Delay(1500);
            }
        }

        return null;
    }
}
