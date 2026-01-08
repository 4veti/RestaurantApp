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
        GetDrinkItemsAsync().GetAwaiter().GetResult();
        GetFoodItemsAsync().GetAwaiter().GetResult();
        GetFoodTypes().GetAwaiter().GetResult();
        GetDrinkTypesAsync().GetAwaiter().GetResult();
    }
    public bool IsFoodsNotBusy => !IsFoodsBusy;
    public bool IsDrinksNotBusy => !IsDrinksBusy;

    public ObservableCollection<DrinkDto> DrinksList { get; } = new();
    public ObservableCollection<FoodDto> FoodList { get; } = new();
    public ObservableCollection<FoodTypeDto> FoodTypesList { get; } = new();
    public ObservableCollection<DrinkTypeDto> DrinkTypesList { get; } = new();

    public ObservableCollection<OrderDto> Orders { get; set; } = new();

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

    public async Task GetFoodTypes()
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
        FoodDto updatedFood = new FoodDto()
        {
            Id = SelectedFood.Id,
            Name = SelectedFood.Name,
            NetGrams = SelectedFood.NetGrams,
            Price = SelectedFood.Price,
            FoodTypeId = SelectedFoodType.Id,
            FoodTypeName = SelectedFoodType.Name,
        };

        await _service.UpdateFood(updatedFood);
        await GetFoodItemsAsync();
    }

    [RelayCommand]
    public async Task UpdateDrink()
    {
        DrinkDto updatedDrink = new DrinkDto()
        {
            Id = SelectedDrink.Id,
            Name = SelectedDrink.Name,
            Millilitres = SelectedDrink.Millilitres,
            Price = SelectedDrink.Price,
            IsAlcoholic = SelectedDrink.IsAlcoholic,
            AlcoholPercentage = SelectedDrink.AlcoholPercentage,
            DrinkTypeId = SelectedDrinkType.Id,
            DrinkType = SelectedDrinkType.Name
        };

        await _service.UpdateDrink(updatedDrink);
        await GetDrinkItemsAsync();
    }

    public async Task<bool?> AnyNewOrders()
    {
        bool? anyNewOrders = await _service.AnyNewOrders(_lastOrderId);

        return anyNewOrders;
    }

    public async Task GetNewOrders()
    {
        List<OrderDto> newOrders = await _service.GetNewOrders(_lastOrderId);

        if (!newOrders.Any())
        {
            return;
        }

        foreach (OrderDto order in newOrders)
        {
            Orders.Add(order);
        }

        _lastOrderId = Orders.Max(o => o.Id);
    }

    [RelayCommand]
    private async Task MarkAsPaid(OrderDto orderToMark)
    {
        bool success = await _service.MarkOrderAsPaid(orderToMark.Id);

        if (success)
        {
            Orders.Remove(orderToMark);
        }
    }
}
