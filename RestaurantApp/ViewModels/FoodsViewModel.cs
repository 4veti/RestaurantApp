using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Models;
using RestaurantApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace RestaurantApp.ViewModels;

public partial class FoodsViewModel : ObservableObject
{
    private readonly RestaurantService _service;

    [ObservableProperty]
    decimal totalPrice;

    [ObservableProperty]
    private bool isBusy;

    public FoodsViewModel(RestaurantService service)
    {
        _service = service;
    }

    public ObservableCollection<FoodDto> FoodList { get; } = new ();
    public ObservableCollection<FoodItemModel> MyFoods { get; } = new();
    public ObservableCollection<DrinkItemModel> MyDrinks { get; } = new();

    public bool IsNotBusy => !IsBusy;

    [RelayCommand]
    private async Task AddFoodToOrderAsync(FoodDto foodDto)
    {
        FoodDto? targetFoodDto = _service.ClientOrder.Foods.FirstOrDefault(x => x.Id == foodDto.Id);

        if (targetFoodDto is not null)
        {
            if (targetFoodDto.Count < 15)
            {
                targetFoodDto.Count++;
                MyFoods.First(f => f.Id == foodDto.Id).Count++;
            }
        }
        else
        {
            MyFoods.Add(new FoodItemModel(foodDto));
            _service.ClientOrder.Foods.Add(foodDto);
        }
    }

    [RelayCommand]
    public async Task GetFoodItemsAsync()
    {
        if (IsBusy)
            return;

        if (!_service.ReloadFoods)
            return;

        try
        {
            IsBusy = true;

            List<FoodDto> foodItems = await _service.GetFoodItemsAsync();

            if (FoodList.Any())
                FoodList.Clear();

            foreach (FoodDto foodItem in foodItems)
            {
                FoodList.Add(foodItem);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlert("Грешка!", "Неуспешно зареждане на ястията.", "Добре");
        }
        finally
        {
            IsBusy = false;
            _service.ReloadFoods = false;
        }
    }

    public void LoadOrderItems()
    {
        MyFoods.Clear();
        MyDrinks.Clear();

        foreach (FoodDto food in _service.ClientOrder.Foods)
        {
            MyFoods.Add(new FoodItemModel(food));
        }

        foreach (DrinkDto drink in _service.ClientOrder.Drinks)
        {
            MyDrinks.Add(new DrinkItemModel(drink));
        }
    }
}