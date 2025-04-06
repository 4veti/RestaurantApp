using RestaurantApp.Views;

namespace RestaurantApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

           Routing.RegisterRoute(nameof(MyOrderPage), typeof(MyOrderPage));
        }
    }
}
