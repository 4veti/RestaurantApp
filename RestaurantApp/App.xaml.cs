using Microsoft.Extensions.Options;
using RestaurantApp.Models;
using RestaurantApp.ViewModels;

namespace RestaurantApp;

public partial class App : Application
{
    public App(MainPageViewModel MainPageViewModel,
            KitchenOrdersPageViewModel kitchenOrdersPageViewModel,
            FrontOfficeViewModel frontOfficeViewModel,
            IOptions<ApplicationSettings> options)
    {
        InitializeComponent();

        MainPage = new AppShell(MainPageViewModel, kitchenOrdersPageViewModel, frontOfficeViewModel, options);
    }
}
