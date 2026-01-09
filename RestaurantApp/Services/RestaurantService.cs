using System.Diagnostics;
using System.Net.Http.Json;
using RestaurantApp.Domain.Contracts.DTOs;

namespace RestaurantApp.Services;

public class RestaurantService
{
    private HttpClient _httpClient;
    private MenuDto _menu;
    private bool completedOrder;

    public RestaurantService()
    {
        _httpClient = new HttpClient();
        ClientOrder = new OrderDto();
        LoadMenu();
    }

    public OrderDto ClientOrder { get; set; }
    public bool ReloadDrinks { get; set; } = true;
    public bool ReloadFoods { get; set; } = true;

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
            ReloadFoods = true;

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
            response.EnsureSuccessStatusCode();

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

    public async Task<bool> UpdateDrink(DrinkDto drinkDto)
    {
        try
        {
            string url = "http://localhost:5000/FrontDesk/Drink";

            var response = await _httpClient.PutAsync(url, JsonContent.Create(drinkDto));
            response.EnsureSuccessStatusCode();

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
            await Shell.Current.DisplayAlert("Грешка!", $"Неуспешна промяна на напитка. {ex.Message}", "Добре");
            return false;
        }
    }

    public async Task<bool?> AnyNewOrders(int lastOrderId)
    {
        try
        {
            string url = $"http://localhost:5000/Kitchen/AnyOrders?lastOrderId={lastOrderId}";
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

            HttpResponseMessage? response = await _httpClient.PostAsync(url, JsonContent.Create(orderId));
            response?.EnsureSuccessStatusCode();

            for (int i = 0; i < 2; i++)
            {
                if (response?.IsSuccessStatusCode ?? false)
                {
                    return true;
                }

                Thread.Sleep(1500);

                response = await _httpClient.PostAsync(url, JsonContent.Create(orderId));
                response?.EnsureSuccessStatusCode();
            }

            Debug.WriteLine("Response to mark order as served not successful " + response?.StatusCode);
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

            HttpResponseMessage? response = await _httpClient.PutAsync(url, JsonContent.Create(orderId));
            response?.EnsureSuccessStatusCode();

            for (int i = 0; i < 2; i++)
            {
                if (response?.IsSuccessStatusCode ?? false)
                {
                    return true;
                }

                Thread.Sleep(1500);

                response = await _httpClient.PostAsync(url, JsonContent.Create(orderId));
                response?.EnsureSuccessStatusCode();
            }

            Debug.WriteLine("Response to mark order as paid not successful " + response?.StatusCode);
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
