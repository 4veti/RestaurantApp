using System.Text.Json;
using RestaurantApp.Models;
using RestaurantApp.ViewModels;
using RestaurantApp.Views;

namespace RestaurantApp
{
    public partial class AppShell : Shell
    {
        public AppShell(FoodsViewModel foodsViewModel,
            DrinksViewModel drinksViewModel,
            MyOrderViewModel myOrderViewModel,
            KitchenOrdersPageViewModel kitchenOrdersPageViewModel)
        {
            string content = File.ReadAllText("D:\\VisualStudio\\Thesis\\RestaurantApp\\Resources\\Raw\\appsettings.txt");
            ApplicationSettings settings = JsonSerializer.Deserialize<ApplicationSettings>(content) ?? new ApplicationSettings() { RunMode = RunMode.Client };

            if (settings.RunMode == RunMode.Kitchen)
            {
                CreateKitchenInterface(kitchenOrdersPageViewModel);
            }
            else if (settings.RunMode == RunMode.FrontDesk)
            {

            }
            else
            {
                CreateClientOrderInterface(foodsViewModel, drinksViewModel, myOrderViewModel);
            }

            InitializeComponent();
        }

        private void CreateKitchenInterface(KitchenOrdersPageViewModel viewModel)
        {
            var mainTab = new TabBar
            {
                Title = "Поръчки кухня",
                Route = "KitchenOrdersTab"
            };
            // Create tab for dishes
            var ordersTab = new Tab
            {
                Title = "Поръчки",
                Route = "KitchenOrdersPage"
            };
            ordersTab.Items.Add(new ShellContent
            {
                Title = "Основна страница поръчки",
                Route = "DishesTabRoute",
                Content = new KitchenOrdersPage(viewModel)
            });
            // Add tabs to MainTab item
            mainTab.Items.Add(ordersTab);


            // Add MainTab item to shell
            Items.Add(mainTab);

            Routing.RegisterRoute(nameof(KitchenOrdersPage), typeof(KitchenOrdersPage));
        }

        private void CreateClientOrderInterface(FoodsViewModel foodsViewModel,
            DrinksViewModel drinksViewModel,
            MyOrderViewModel myOrderViewModel)
        {
            var mainTab = new TabBar
            {
                Title = "TestRestaurantTab",
                Route = "testRestaurantTab"
            };

            // Create tab for dishes
            var dishesTab = new Tab
            {
                Title = "Ястия",
                Route = "MainPage"
            };
            // Add the dishes tab
            dishesTab.Items.Add(new ShellContent
            {
                Title = "Основна страница ястия",
                Route = "DishesTabRoute",
                Content = new MainPage(foodsViewModel)
            });
            // Add tabs to MainTab item
            mainTab.Items.Add(dishesTab);


            // Create tab for drinks
            var drinksTab = new Tab
            {
                Title = "Напитки",
                Route = "DrinksTabRoute"
            };
            // Add the drinks tab
            drinksTab.Items.Add(new ShellContent
            {
                Title = "Напитки",
                Route = "DrinksPage",
                Content = new DrinksPage(drinksViewModel)
            });
            // Add tabs to MainTab item
            mainTab.Items.Add(drinksTab);



            // Create tab for orders
            var ordersTab = new Tab
            {
                Title = "Данни за поръчка",
                Route = "OrdersTabRoute"
            };
            // Add the orders tab
            ordersTab.Items.Add(new ShellContent
            {
                Title = "Основна страница поръчка",
                Route = "MyOrderPage",
                Content = new MyOrderPage(myOrderViewModel)
            });
            // Add tabs to MainTab item
            mainTab.Items.Add(ordersTab);



            // Add MainTab item to shell
            Items.Add(mainTab);

            Routing.RegisterRoute(nameof(MyOrderPage), typeof(MyOrderPage));
        }
    }
}
