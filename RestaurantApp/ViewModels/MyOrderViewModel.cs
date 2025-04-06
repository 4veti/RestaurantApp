using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Services;

namespace RestaurantApp.ViewModels;

public partial class MyOrderViewModel : ObservableObject
{
    private readonly RestaurantService _service;

    public MyOrderViewModel(RestaurantService service)
    {
        _service = service;
        LoadOrderItems();
    }

    public ObservableCollection<FoodDto> MyFoods { get; } = new();
    public ObservableCollection<DrinkDto> MyDrinks { get; } = new();

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    public void LoadOrderItems()
    {
        MyFoods.Clear();
        MyDrinks.Clear();

        foreach (FoodDto food in _service.ClientOrder.Foods)
        {
            MyFoods.Add(food);
        }

        foreach (DrinkDto drink in _service.ClientOrder.Drinks)
        {
            MyDrinks.Add(drink);
        }
    }
}
