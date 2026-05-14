using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Models;
using RestaurantApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace RestaurantApp.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    private readonly RestaurantService _service;

    [ObservableProperty]
    decimal totalPrice;

    [ObservableProperty]
    private bool isBusy;

    public MainPageViewModel(RestaurantService service)
    {
        _service = service;
    }

    public ObservableCollection<FoodDto> FoodList { get; } = new ();
    public ObservableCollection<DrinkDto> DrinksList { get; } = new ();
    public ObservableCollection<FoodItemModel> MyFoods { get; } = new();
    public ObservableCollection<DrinkItemModel> MyDrinks { get; } = new();

    public bool IsNotBusy => !IsBusy;

    [RelayCommand]
    private void AddFoodToOrder(FoodDto foodDto)
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
    private void AddDrinkToOrder(DrinkDto drinkDto)
    {
        DrinkDto? targetDrinkDto = _service.ClientOrder.Drinks.FirstOrDefault(x => x.Id == drinkDto.Id);

        if (targetDrinkDto is not null)
        {
            if (targetDrinkDto.Count < 15)
            {
                targetDrinkDto.Count++;
                MyDrinks.First(f => f.Id == drinkDto.Id).Count++;
            }
        }
        else
        {
            MyDrinks.Add(new DrinkItemModel(drinkDto));
            _service.ClientOrder.Drinks.Add(drinkDto);
        }
    }

    public void ClearOrderIfEmpty()
    {
        if (_service.ClientOrder.Foods.Any() == false && _service.ClientOrder.Foods.Any() == false)
        {
            MyFoods.Clear();
            MyDrinks.Clear();
        }
    }

    public async Task GetMenutemsAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            List<FoodDto> foodItems = await _service.GetFoodItemsAsync();
            List<DrinkDto> drinkItems = await _service.GetDrinkItemsAsync();

            if (FoodList.Any())
                FoodList.Clear();

            if (DrinksList.Any())
                DrinksList.Clear();
            
            foreach (FoodDto foodItem in foodItems)
            {
                FoodList.Add(foodItem);
            }

            foreach (DrinkDto drinkItem in drinkItems)
            {
                DrinksList.Add(drinkItem);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlert("Грешка!", "Неуспешно зареждане на менюто.", "Добре");
        }
        finally
        {
            IsBusy = false;
        }
    }
}