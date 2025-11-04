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
    [ObservableProperty]
    private bool isFoodsBusy;

    [ObservableProperty]
    private bool isDrinksBusy;

    private bool isFoodTypesBusy;

    private readonly RestaurantService _service;

    public FrontOfficeViewModel(RestaurantService service)
    {
        _service = service;
        GetDrinkItemsAsync().GetAwaiter().GetResult();
        GetFoodItemsAsync().GetAwaiter().GetResult();
        GetFoodTypes().GetAwaiter().GetResult();
    }
    public bool IsFoodsNotBusy => !IsFoodsBusy;
    public bool IsDrinksNotBusy => !IsDrinksBusy;

    public ObservableCollection<DrinkDto> DrinksList { get; } = new();
    public ObservableCollection<FoodDto> FoodList { get; } = new();
    public ObservableCollection<FoodTypeDto> FoodTypesList { get; } = new();

    [RelayCommand]
    public async Task GetDrinkItemsAsync()
    {
        if (IsDrinksBusy)
            return;

        try
        {
            IsDrinksBusy = true;

            List<DrinkDto> DrinkItems = await _service.GetDrinkItemsAsync();

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


    [RelayCommand]
    public async Task GetFoodItemsAsync()
    {
        if (IsFoodsBusy)
            return;

        try
        {
            IsFoodsBusy = true;

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
            IsFoodsBusy = false;
        }
    }

    //[RelayCommand]
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
}
