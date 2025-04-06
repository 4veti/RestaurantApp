using System.Net.Http.Json;
using RestaurantApp.Domain.Contracts.DTOs;

namespace RestaurantApp.Services;

public class RestaurantService
{
    private HttpClient _httpClient;
    private MenuDto _menu;

    public RestaurantService()
    {
        _httpClient = new HttpClient();                 // 
        ClientOrder = new OrderDto();                   //
        LoadMenu();                                     // Load once on start, and then after completing an order set a boolean flag that an order has been completed. Next time the menu is requested, get from API if flag is true.
    }

    public OrderDto ClientOrder { get; set; }

    public async Task<List<FoodDto>> GetFoodItemsAsync()
    {
        if (_menu.Foods.Any())
        {
            LoadMenu();
        }

        return _menu.Foods.ToList();
    }

    public async Task<List<DrinkDto>> GetDrinkItemsAsync()
    {
        if (_menu.Foods.Any())
        {
            LoadMenu();
        }

        return _menu.Drinks.ToList();
    }

    private void LoadMenu()
    {
        try
        {
            string url = "http://localhost:5000/Client/Menu";
            var response = _httpClient.GetAsync(url).Result;

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
    }
}
