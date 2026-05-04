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
    private MenuDto _menu;
    private string apiBaseAddress;
    private bool completedOrder;

    public RestaurantService(IOptions<ApplicationSettings> options)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(options.Value.BaseAPIUrl);
        ClientOrder = new OrderDto();
        LoadMenu();
    }

    public OrderDto ClientOrder { get; set; }
    public bool ReloadDrinks { get; set; } = true;
    public bool ReloadMenu { get; set; } = true;

    public async Task<List<FoodDto>> GetFoodItemsAsync(bool forceReload = false)
    {
        if (!_menu.Foods.Any() || completedOrder || forceReload)
        {
            LoadMenu();
        }

        return _menu.Foods.ToList();
    }

    public async Task<List<DrinkDto>> GetDrinkItemsAsync(bool forceReload = false)
    {
        if (!_menu.Drinks.Any() || completedOrder || forceReload)
        {
            LoadMenu();
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
        try
        {
            DateTime now = DateTime.Now;
            ClientOrder.OrderName = $"{now.Hour}{now.Minute}{now.Second}";

            string url = "http://localhost:5000/Client/Order";

            var response = await _httpClient.PostAsync(url, JsonContent.Create(ClientOrder));
            response.EnsureSuccessStatusCode();

            if (!(response?.IsSuccessStatusCode ?? false))
            {
                Debug.WriteLine("Response to place order not successful " + response?.StatusCode);
                await Shell.Current.DisplayAlert("Грешка!", "Неуспешно извършване на поръчка.", "Добре");
                return false;
            }

            ClientOrder = new OrderDto();
            completedOrder = true;
            ReloadDrinks = true;
            ReloadMenu = true;

            LoadMenu();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlert("Грешка!", $"Неуспешно извършване на поръчка. {ex.Message}", "Добре");
            return false;
        }
    }

    public async Task<bool> UpdateFood(FoodDto foodDto)
    {
        try
        {
            string url = "http://localhost:5000/FrontDesk/Food";

            var response = await _httpClient.PutAsync(url, JsonContent.Create(foodDto));

            if (!(response?.IsSuccessStatusCode ?? false))
            {
                Debug.WriteLine("Response to update dish not successful " + response?.StatusCode);
                await Shell.Current.DisplayAlert("Грешка!", "Неуспешна промяна на ястие.", "Добре");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlert("Грешка!", $"Неуспешна промяна на ястие. {ex.Message}", "Добре");
            return false;
        }
    }

    public async Task<bool> AddFood(FoodDto foodDto)
    {
        try
        {
            string url = "http://localhost:5000/FrontDesk/Food";

            var response = await _httpClient.PostAsync(url, JsonContent.Create(foodDto));

            if (!(response?.IsSuccessStatusCode ?? false))
            {
                Debug.WriteLine("Response to create food not successful " + response?.StatusCode);
                await Shell.Current.DisplayAlert("Грешка!", "Неуспешно добавяне на ястие.", "Добре");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> UpdateDrink(DrinkDto drinkDto)
    {
        try
        {
            string url = "http://localhost:5000/FrontDesk/Drink";

            var response = await _httpClient.PutAsync(url, JsonContent.Create(drinkDto));

            if (!(response?.IsSuccessStatusCode ?? false))
            {
                Debug.WriteLine("Response to update drink not successful " + response?.StatusCode);
                await Shell.Current.DisplayAlert("Грешка!", "Неуспешна промяна на напитка.", "Добре");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> AddDrink(DrinkDto drinkDto)
    {
        try
        {
            string url = "http://localhost:5000/FrontDesk/Drink";

            var response = await _httpClient.PostAsync(url, JsonContent.Create(drinkDto));

            if (!(response?.IsSuccessStatusCode ?? false))
            {
                Debug.WriteLine("Response to create drink not successful " + response?.StatusCode);
                await Shell.Current.DisplayAlert("Грешка!", "Неуспешно добавяне на напитка.", "Добре");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool?> AnyNewOrders(int lastOrderId)
    {
        bool? result = await GetResponseFromApi<bool>(HttpVerb.Get, KITCHEN_CONTROLLER, ApiEntityRoutes.AnyOrders, [("lastOrderId", lastOrderId)], null);

        return result ?? false;

        try
        {
            string url = $"/Kitchen/AnyOrders?lastOrderId={lastOrderId}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            if ((response?.IsSuccessStatusCode ?? false) == false)
            {
                return null;
            }

            bool anyNewOrders = await response.Content.ReadFromJsonAsync<bool>();
            return anyNewOrders;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<List<OrderDto>> GetNewOrders(int lastOrderId)
    {
        try
        {
            string url = $"http://localhost:5000/Kitchen/Orders?lastOrderId={lastOrderId}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            List<OrderDto> orders = new();

            if (response?.IsSuccessStatusCode ?? false)
            {
                orders = await response.Content.ReadFromJsonAsync<List<OrderDto>>() ?? new();
            }

            return orders;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlert("Грешка!", $"Неуспешно зареждане на поръчки. {ex.Message}", "Добре");
            return new();
        }
    }

    public async Task<List<OrderDto>> GetNewOrdersForFrontDesk(int lastOrderId)
    {
        try
        {
            string url = $"http://localhost:5000/FrontDesk/Orders?lastOrderId={lastOrderId}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            List<OrderDto> orders = new();

            if (response?.IsSuccessStatusCode ?? false)
            {
                orders = await response.Content.ReadFromJsonAsync<List<OrderDto>>() ?? new();
            }

            return orders;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlert("Грешка!", $"Неуспешно зареждане на поръчки. {ex.Message}", "Добре");
            return new();
        }
    }

    public async Task<bool> MarkOrderAsServed(int orderId)
    {
        try
        {
            string url = "http://localhost:5000/Kitchen/OrderServed";
            HttpResponseMessage? response = null;

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    response = await _httpClient.PostAsync(url, JsonContent.Create(orderId));
                    response.EnsureSuccessStatusCode();

                    return true;
                }
                catch (HttpRequestException)
                {
                    Debug.WriteLine("Response to mark order as served not successful " + response?.StatusCode);

                    if (i == 2) throw;
                    await Task.Delay(1500);
                }
            }

            await Shell.Current.DisplayAlert("Грешка!", "Неуспешно маркиране на поръчка като готова.", "Добре");
            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlert("Грешка!", $"Неуспешно маркиране на поръчка като готова: {ex.Message}", "Добре");
            return false;
        }
    }

    public async Task<bool> MarkOrderAsPaid(int orderId)
    {
        try
        {
            string url = "http://localhost:5000/FrontDesk/OrderPaid";

            HttpResponseMessage? response = null;

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    response = await _httpClient.PutAsync(url, JsonContent.Create(orderId));
                    response.EnsureSuccessStatusCode();

                    return true;
                }
                catch (HttpRequestException)
                {
                    Debug.WriteLine("Response to mark order as paid not successful " + response?.StatusCode);

                    if (i == 2) throw;
                    await Task.Delay(1500);
                }
            }

            await Shell.Current.DisplayAlert("Грешка!", "Неуспешно маркиране на поръчка като платена.", "Добре");
            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlert("Грешка!", $"Неуспешно маркиране на поръчка като платена: {ex.Message}", "Добре");
            return false;
        }
    }

    public async Task<List<FoodTypeDto>> GetFoodTypes()
    {
        try
        {
            string url = $"http://localhost:5000/FrontDesk/FoodType";
            var response = _httpClient.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();

            if (response?.IsSuccessStatusCode ?? false)
            {
                return await response.Content.ReadFromJsonAsync<List<FoodTypeDto>>() ?? new();
            }

            return new();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new();
        }
    }

    public async Task<List<DrinkTypeDto>> GetDrinkTypes()
    {
        List<DrinkTypeDto>? result = await GetResponseFromApi<List<DrinkTypeDto>>(HttpVerb.Get, FRONTDESK_CONTROLLER, ApiEntityRoutes.DrinkType, null, null);

        return result ?? new();

        try
        {
            string url = $"http://localhost:5000/FrontDesk/DrinkType";
            var response = _httpClient.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();

            if (response?.IsSuccessStatusCode ?? false)
            {
                return await response.Content.ReadFromJsonAsync<List<DrinkTypeDto>>() ?? new();
            }

            return new();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new();
        }
    }

    private async Task<T?> GetResponseFromApi<T>(HttpVerb httpVerb, string controller, string targetEntity, IEnumerable<(string, object)>? queryParams = null, object? content = null)
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
                Debug.WriteLine($"Response to mark order as paid not successful. StatusCode: {response?.StatusCode}; Msg: {ex.Message}");

                if (i == 2) throw;
                await Task.Delay(1500);
            }
        }

        return null;
    }

    private void LoadMenu()
    {
        try
        {
            string url = "http://localhost:5000/Client/Menu";
            var response = _httpClient.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();

            if (response?.IsSuccessStatusCode ?? false)
            {
                _menu = response.Content.ReadFromJsonAsync<MenuDto>()?.Result ?? new MenuDto();
            }

            if (_menu is null)
            {
                _menu = new MenuDto();
            }
        }
        catch (Exception)
        {
            _menu = new MenuDto();
        }
        finally
        {
            completedOrder = false;
        }
    }
}
