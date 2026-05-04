using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Models;
using RestaurantApp.Services;

namespace RestaurantApp.ViewModels;

public partial class FrontOfficeViewModel : ObservableObject
{
    private int _lastOrderId = 0;

    [ObservableProperty]
    private bool isFoodsBusy;

    [ObservableProperty]
    private bool isDrinksBusy;

    private bool isFoodTypesBusy;

    private bool isDrinkTypesBusy;

    private bool initiatedFoods = false;
    private bool initiatedDrinks = false;


    [ObservableProperty]
    private FoodDto selectedFood;

    [ObservableProperty]
    private FoodTypeDto selectedFoodType;

    [ObservableProperty]
    private DrinkDto selectedDrink;

    [ObservableProperty]
    private DrinkTypeDto selectedDrinkType;

    private readonly RestaurantService _service;

    public FrontOfficeViewModel(RestaurantService service)
    {
        _service = service;
    }
    public bool IsFoodsNotBusy => !IsFoodsBusy;
    public bool IsDrinksNotBusy => !IsDrinksBusy;

    public ObservableCollection<DrinkDto> DrinksList { get; } = new();
    public ObservableCollection<FoodDto> FoodList { get; } = new();
    public ObservableCollection<FoodTypeDto> FoodTypesList { get; } = new();
    public ObservableCollection<DrinkTypeDto> DrinkTypesList { get; } = new();
    public ObservableCollection<OrderDto> ActiveFrontDeskOrders { get; set; } = new();
    public ObservableCollection<OrderDto> PaidOrders { get; set; } = new();

    public async Task InitializeFodsAsync()
    {
        if (initiatedFoods)
        {
            return;
        }

        initiatedFoods = true;
        await GetFoodItemsAsync();
        await GetFoodTypesAsync();
    }

    public async Task InitializeDrinksAsync()
    {
        if (initiatedDrinks)
        {
            return;
        }

        initiatedDrinks = true;
        await GetDrinkItemsAsync();
        await GetDrinkTypesAsync();
    }

    [RelayCommand]
    public async Task GetDrinkItemsAsync()
    {
        if (IsDrinksBusy)
            return;

        try
        {
            IsDrinksBusy = true;

            List<DrinkDto> DrinkItems = await _service.GetDrinkItemsAsync(true);

            if (DrinksList.Any())
                DrinksList.Clear();

            foreach (DrinkDto drinkItem in DrinkItems)
            {
                DrinksList.Add(drinkItem);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlert("Грешка!", "Неуспешно зареждане на напитките.", "Добре");
        }
        finally
        {
            IsDrinksBusy = false;
        }
    }

    public async Task GetDrinkTypesAsync()
    {
        if (isDrinkTypesBusy)
            return;

        try
        {
            isDrinkTypesBusy = true;

            List<DrinkTypeDto> drinkTypes = await _service.GetDrinkTypes();

            if (DrinkTypesList.Any())
                DrinkTypesList.Clear();

            foreach (DrinkTypeDto drinkType in drinkTypes)
            {
                DrinkTypesList.Add(drinkType);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlert("Грешка!", "Неуспешно зареждане на типовете напитки.", "Добре");
        }
        finally
        {
            isDrinkTypesBusy = false;
        }
    }

    [RelayCommand]
    public async Task GetFoodItemsAsync()
    {
        if (IsFoodsBusy)
            return;

        try
        {
            IsFoodsBusy = true;

            List<FoodDto> foodItems = await _service.GetFoodItemsAsync(true);

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
            IsFoodsBusy = false;
        }
    }

    public async Task GetFoodTypesAsync()
    {
        if (isFoodTypesBusy)
            return;

        try
        {
            isFoodTypesBusy = true;

            List<FoodTypeDto> foodTypes = await _service.GetFoodTypes();

            if (FoodTypesList.Any())
                FoodTypesList.Clear();

            foreach (FoodTypeDto foodtype in foodTypes)
            {
                FoodTypesList.Add(foodtype);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlert("Грешка!", "Неуспешно зареждане на типовете ястия.", "Добре");
        }
        finally
        {
            isFoodTypesBusy = false;
        }
    }

    [RelayCommand]
    public async Task UpdateFood()
    {
        bool success = false;

        if (SelectedFood.Id > 0)
        {
            success = await _service.UpdateFood(SelectedFood);
        }
        else
        {
            success = await _service.AddFood(SelectedFood);
        }

        if (success)
        {
            await GetFoodItemsAsync();
        }
    }

    [RelayCommand]
    public async Task UpdateDrink()
    {
        bool success = false;

        if (SelectedDrink.Id > 0)
        {            
            success = await _service.UpdateDrink(SelectedDrink);
        }
        else
        {
            success = await _service.AddDrink(SelectedDrink);
        }

        if (success)
        {
            await GetDrinkItemsAsync();
        }
    }

    public async Task GetNewOrders()
    {
        List<OrderDto> newOrders = await _service.GetNewOrdersForFrontDesk(_lastOrderId);

        if (!newOrders.Any())
        {
            return;
        }

        foreach (OrderDto order in newOrders)
        {
            if (order.IsPaid)
            {
                PaidOrders.Add(order);
            }
            else
            {
                ActiveFrontDeskOrders.Add(order);
            }

            if (order.Id > _lastOrderId)
            {
                _lastOrderId = order.Id;
            }
        }
    }

    [RelayCommand]
    private async Task MarkAsPaid(OrderDto orderToMark)
    {
        bool success = await _service.MarkOrderAsPaid(orderToMark.Id);

        if (success)
        {
            ActiveFrontDeskOrders.Remove(orderToMark);
            PaidOrders.Add(orderToMark);
        }
    }
}
