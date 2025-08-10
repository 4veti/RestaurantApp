using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantApp.Domain.Contracts.DTOs;
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
        GetDrinkItemsAsync();
    }

    public ObservableCollection<DrinkDto> DrinksList { get; } = new ();

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
            }
        }
        else
        {
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
}