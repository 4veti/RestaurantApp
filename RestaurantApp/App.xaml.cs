using System.Text.Json;
using RestaurantApp.Models;
using RestaurantApp.ViewModels;

namespace RestaurantApp;

public partial class App : Application
{
    public App(FoodsViewModel foodsViewModel,
            MyOrderViewModel myOrderViewModel,
            KitchenOrdersPageViewModel kitchenOrdersPageViewModel,
            FrontOfficeViewModel frontOfficeViewModel)
    {
        InitializeComponent();

        MainPage = new AppShell(foodsViewModel, myOrderViewModel, kitchenOrdersPageViewModel, frontOfficeViewModel);
    }
}
