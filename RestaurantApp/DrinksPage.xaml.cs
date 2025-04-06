using RestaurantApp.ViewModels;

namespace RestaurantApp;

public partial class DrinksPage : ContentPage
{
	public DrinksPage(DrinksViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
}