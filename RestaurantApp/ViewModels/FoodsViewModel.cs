using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Services;
using RestaurantApp.Views;

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
        GetFoodItemsAsync();
    }

    public ObservableCollection<FoodDto> FoodList { get; } = new ();

    public bool IsNotBusy => !IsBusy;

    [RelayCommand]
    private async Task AddFoodToOrderAsync(FoodDto foodDto)
    {
        FoodDto? targetFoodDto = _service.ClientOrder.Foods.FirstOrDefault(x => x.Id == foodDto.Id);

        if (targetFoodDto is not null)
        {
            targetFoodDto.Count++;
        }
        else
        {
            _service.ClientOrder.Foods.Add(foodDto);
        }
    }

    [RelayCommand]
    private async Task GetFoodItemsAsync()
    {
        if (IsBusy)
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
        }
    }

    [RelayCommand]
    private async Task GoToMyOrderPageAsync()
    {
        await Shell.Current.GoToAsync(nameof(MyOrderPage), true);
    }
}