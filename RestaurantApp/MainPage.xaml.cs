using RestaurantApp.ViewModels;

namespace RestaurantApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
           BindingContext = viewModel;
        }

        private void ButtonStartOrder_Clicked(object sender, EventArgs e)
        {
            gWelcomeScreen.IsEnabled = false;
            gWelcomeScreen.IsVisible = false;
            fWelcomeScreenShade.IsEnabled = false;
            fWelcomeScreenShade.IsVisible = false;
        }
    }

}
