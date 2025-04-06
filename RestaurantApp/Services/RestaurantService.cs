using System.Net.Http.Json;
using RestaurantApp.Domain.Contracts.DTOs;

namespace RestaurantApp.Services;

public class RestaurantService
{
    private HttpClient _httpClient;
    private MenuDto? _menu;

    public RestaurantService()
    {
        _httpClient = new HttpClient();
        _menu = new MenuDto();
        ClientOrder = new OrderDto();
    }
    
    public OrderDto ClientOrder { get; set; }

    public async Task<List<FoodDto>> GetFoodItemsAsync()
    {
        await LoadMenu(); //Maybe don't load every time

        if (_menu is null)
        {
            return new List<FoodDto>();
        }

        return _menu.Foods.ToList();
    }

    public async Task<List<DrinkDto>> GetDrinkItemsAsync()
    {
        await LoadMenu(); //Maybe don't load every time

        if (_menu is null)
        {
            return new List<DrinkDto>();
        }

        return _menu.Drinks.ToList();
    }

    private async Task LoadMenu()
    {
        string url = "http://localhost:5000/Client/Menu";
        var response = await _httpClient.GetAsync(url);

        if (response?.IsSuccessStatusCode ?? false)
        {
            _menu = await response.Content.ReadFromJsonAsync<MenuDto>();
        }
    }
}
