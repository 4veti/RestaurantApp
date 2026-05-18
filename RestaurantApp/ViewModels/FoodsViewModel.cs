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

        TotalPrice += foodModel.Price;
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
        TotalPrice -= foodModel.Price;
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

        TotalPrice += drinkModel.Price;
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
        TotalPrice -= drinkModel.Price;

    }

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

        TotalPrice += foodDto.Price;
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

        TotalPrice += drinkDto.Price;
    }

    [RelayCommand]
    private async Task PlaceOrder()
    {
        if (MyFoods.Any() || MyDrinks.Any())
        {
            bool orderSuccess = await _service.PlaceOrder();

            if (orderSuccess)
            {
                MyFoods.Clear();
                MyDrinks.Clear();

                SetTotalPrice();
            }

            return;
        }

        await Shell.Current.DisplayAlert("Грешка!", "Трябва да имате поне един избран елемент, за да направите поръчка.", "Добре");
    }

    public void SetTotalPrice()
    {
        TotalPrice = MyFoods.Sum(x => x.Price * x.Count) + MyDrinks.Sum(x => x.Price * x.Count);
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