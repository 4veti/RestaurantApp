using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Models;
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

    public ObservableCollection<FoodItemModel> MyFoods { get; } = new();
    public ObservableCollection<DrinkItemModel> MyDrinks { get; } = new();

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

    [RelayCommand]
    private void IncreaseFoodItemCount(int foodId)
    {
        FoodItemModel foodModel = MyFoods.First(x => x.Id == foodId);

        if (foodModel.Count < 15)
        {
            foodModel.Count++;
            _service.SetFoodItemCount(foodId, foodModel.Count);
        }
    }

    [RelayCommand]
    private void DecreaseFoodItemCount(int foodId)
    {
        FoodItemModel foodModel = MyFoods.First(x => x.Id == foodId);
        foodModel.Count--;

        if (foodModel.Count < 1)
        {
            MyFoods.Remove(foodModel);
        }

        _service.SetFoodItemCount(foodId, foodModel.Count);
    }

    [RelayCommand]
    private void IncreaseDrinkItemCount(int drinkId)
    {
        DrinkItemModel drinkModel = MyDrinks.First(x => x.Id == drinkId);

        if (drinkModel.Count < 15)
        {
            drinkModel.Count++;
            _service.SetDrinkItemCount(drinkId, drinkModel.Count);
        }
    }

    [RelayCommand]
    private void DecreaseDrinkItemCount(int drinkId)
    {
        DrinkItemModel drinkModel = MyDrinks.First(x => x.Id == drinkId);
        drinkModel.Count--;

        if (drinkModel.Count < 1)
        {
            MyDrinks.Remove(drinkModel);
        }

        _service.SetDrinkItemCount(drinkId, drinkModel.Count);
    }

    [RelayCommand]
    private async Task PlaceOrder()
    {
        if (MyFoods.Any() || MyDrinks.Any())
        {
            await _service.PlaceOrder();
            return;
        }

        await Shell.Current.DisplayAlert("Грешка!", "Трябва да имате поне един избран елемент, за да направите поръчка.", "Добре");
    }
}
