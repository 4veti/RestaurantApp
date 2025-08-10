using System.Text.Json;
using RestaurantApp.Models;
using RestaurantApp.ViewModels;

namespace RestaurantApp;

public partial class App : Application
{
    public App(FoodsViewModel foodsViewModel,
            DrinksViewModel drinksViewModel,
            MyOrderViewModel myOrderViewModel)
    {
        InitializeComponent();

        MainPage = new AppShell(foodsViewModel, drinksViewModel, myOrderViewModel);
    }
}
