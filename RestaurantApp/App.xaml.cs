using System.Text.Json;
using RestaurantApp.Models;
using RestaurantApp.ViewModels;

namespace RestaurantApp;

public partial class App : Application
{
    public App(FoodsViewModel foodsViewModel,
            DrinksViewModel drinksViewModel,
            MyOrderViewModel myOrderViewModel,
            KitchenOrdersPageViewModel kitchenOrdersPageViewModel)
    {
        InitializeComponent();

        MainPage = new AppShell(foodsViewModel, drinksViewModel, myOrderViewModel, kitchenOrdersPageViewModel);
    }
}
