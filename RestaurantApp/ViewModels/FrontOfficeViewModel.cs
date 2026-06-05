using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantApp.Client.Models;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Models;
using RestaurantApp.Services;

namespace RestaurantApp.ViewModels;

public partial class FrontOfficeViewModel : ObservableObject
{
    private int _oldestNotServedOrderID = int.MaxValue;
    private int _lastOrderID = 0;

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
    }
    public bool IsFoodsNotBusy => !IsFoodsBusy;
    public bool IsDrinksNotBusy => !IsDrinksBusy;

    public ObservableCollection<DrinkDto> DrinksList { get; } = new();
    public ObservableCollection<FoodDto> FoodList { get; } = new();
    public ObservableCollection<FoodTypeDto> FoodTypesList { get; } = new();
    public ObservableCollection<DrinkTypeDto> DrinkTypesList { get; } = new();
    public ObservableCollection<ExtendedOrderDto> ActiveFrontDeskOrders { get; set; } = new();
    public ObservableCollection<ExtendedOrderDto> PaidOrders { get; set; } = new();

    public async Task InitializeFoodsAsync()
    {
        await GetFoodItemsAsync();
        await GetFoodTypesAsync();
    }

    public async Task InitializeDrinksAsync()
    {
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
        GetOrdersFrontDeskDTO? ordersResponse = await _service.GetNewOrdersForFrontDesk(_oldestNotServedOrderID, _lastOrderID);

        if (ordersResponse is null)
        {
            return;
        }

        if (!string.IsNullOrWhiteSpace(ordersResponse.ErrorMessage))
        {
            Debug.WriteLine(ordersResponse.ErrorMessage);
        }

        List<OrderDto> newOrders = ordersResponse.NewOrders;

        foreach (OrderDto order in newOrders.OrderBy(o => o.Id))
        {
            if (order.IsPaid)
            {
                PaidOrders.Insert(0, new ExtendedOrderDto(order));
            }
            else
            {
                ActiveFrontDeskOrders.Insert(0, new ExtendedOrderDto(order));
            }

            if (order.Id > _lastOrderID)
            {
                _lastOrderID = order.Id;
            }            
        }

        if (ordersResponse.ServedOrderIDs is null)
        {
            return;
        }

        Debug.WriteLine($"New served order IDs: {string.Join(", ", ordersResponse.ServedOrderIDs)}");
        Debug.WriteLine($"Current paid order IDs: {string.Join(", ", PaidOrders.Select(po => po.Id))}");

        foreach (ExtendedOrderDto paidOrder in PaidOrders)
        {
            if (paidOrder.IsServed == false && ordersResponse.ServedOrderIDs.Contains(paidOrder.Id))
            {
                paidOrder.IsServed = true;
            }
            else if (paidOrder.IsServed == false)
            {
                _oldestNotServedOrderID = paidOrder.Id;
            }
        }

        Debug.WriteLine($"Oldest not served order ID: {_oldestNotServedOrderID}");
    }

    [RelayCommand]
    private async Task MarkAsPaid(ExtendedOrderDto orderToMark)
    {
        bool success = await _service.MarkOrderAsPaid(orderToMark.Id);

        if (success)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                ExtendedOrderDto existingOrderToMark = ActiveFrontDeskOrders.First(o => o.Id == orderToMark.Id);
                ActiveFrontDeskOrders.Remove(existingOrderToMark);

                int insertIndex = GetInsertOrderIndex(PaidOrders, orderToMark.Id);
                PaidOrders.Insert(insertIndex, existingOrderToMark);
            });
        }
    }

    private int GetInsertOrderIndex(IEnumerable<ExtendedOrderDto> orders, int insertOrderID)
    {
        int index = 0;

        foreach (ExtendedOrderDto order in orders)
        {
            if (insertOrderID > order.Id)
            {
                break;
            }

            index++;
        }

        return index;
    }
}
