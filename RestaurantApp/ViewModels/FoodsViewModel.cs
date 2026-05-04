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
    private async Task AddFoodToOrderAsync(FoodDto foodDto)
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
    public async Task GetMenutemsAsync()
    {
        if (IsBusy)
            return;

        if (!_service.ReloadMenu)
            return;

        try
        {
            IsBusy = true;

            if (FoodList.Any())
                FoodList.Clear();

            if (DrinksList.Any())
                DrinksList.Clear();

            List<FoodDto> foodItems = await _service.GetFoodItemsAsync();
            foreach (FoodDto foodItem in foodItems)
            {
                FoodList.Add(foodItem);
            }

            List<DrinkDto> drinkItems = await _service.GetDrinkItemsAsync();
            foreach (DrinkDto drinkItem in drinkItems)
            {
                DrinksList.Add(drinkItem);
            }

            //LoadMenuStructureAndItems(foodItems, drinkItems);

            _service.ReloadMenu = !foodItems.Any() && !drinkItems.Any();
        }
        catch (Exception ex)
        {
            _service.ReloadMenu = true;
            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlert("Грешка!", "Неуспешно зареждане на менюто.", "Добре");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void LoadMenuStructureAndItems(List<FoodDto> foods, List<DrinkDto> drinks)
    {
        Dictionary<int, List<FoodDto>> foodsGrouped = foods
            .GroupBy(f => f.Id)
            .OrderBy(x => x.Key)
            .ToDictionary(f => f.Key, f => f.ToList());

        Dictionary<int, List<DrinkDto>> drinksGrouped = drinks
            .GroupBy(d => d.Id)
            .OrderBy(x => x.Key)
            .ToDictionary(d => d.Key, d => d.ToList());

        Frame mainFrame = new();
        ScrollView mainScrollView = new();
        mainFrame.Content = mainScrollView;

        foreach (List<FoodDto> currentFoodsGroup in foodsGrouped.Values)
        {
            var collectionView = new CollectionView();

            collectionView.ItemTemplate = new DataTemplate(() =>
            {
                var nameLabel = new Label { FontSize = 22 };
                nameLabel.SetBinding(Label.TextProperty, "Name");

                var gramsLabel = new Label();
                gramsLabel.SetBinding(Label.TextProperty,
                    new Binding("NetGrams", stringFormat: "{0}gr"));

                var typeLabel = new Label();
                typeLabel.SetBinding(Label.TextProperty, "FoodTypeName");

                var priceLabel = new Label();
                priceLabel.SetBinding(Label.TextProperty,
                    new Binding("Price", stringFormat: "Price: {0:C2}"));

                var stack = new VerticalStackLayout
                {
                    Children =
                    {
                        nameLabel,
                        gramsLabel,
                        typeLabel,
                        priceLabel
                    }
                };

                var frame = new Frame
                {
                    Content = stack
                };

                var tapGesture = new TapGestureRecognizer
                {
                    Command = AddFoodToOrderCommand
                };

                // 🔑 Pass the FoodDto itself
                tapGesture.SetBinding(
                    TapGestureRecognizer.CommandParameterProperty,
                    new Binding("."));

                frame.GestureRecognizers.Add(tapGesture);

                return new Grid
                {
                    Padding = 10,
                    Children = { frame }
                };
            });

            collectionView.ItemsSource = currentFoodsGroup;
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