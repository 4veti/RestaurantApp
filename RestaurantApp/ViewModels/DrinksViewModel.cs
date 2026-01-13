using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Models;
using RestaurantApp.Services;

namespace RestaurantApp.ViewModels;

public partial class DrinksViewModel : ObservableObject
{
    private readonly RestaurantService _service;

    [ObservableProperty]
    decimal totalPrice;

    [ObservableProperty]
    private bool isBusy;

    public DrinksViewModel(RestaurantService service)
    {
        _service = service;
    }

    public ObservableCollection<DrinkDto> DrinksList { get; } = new ();
    public ObservableCollection<FoodItemModel> MyFoods { get; } = new();
    public ObservableCollection<DrinkItemModel> MyDrinks { get; } = new();

    public bool IsNotBusy => !IsBusy;

    [RelayCommand]
    private async Task AddDrinkToOrderAsync(DrinkDto drinkDto)
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

    [RelayCommand]
    public async Task GetDrinkItemsAsync()
    {
        if (IsBusy)
            return;

        if (!_service.ReloadDrinks)
            return;

        try
        {
            IsBusy = true;

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
            IsBusy = false;
            _service.ReloadDrinks = false;
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