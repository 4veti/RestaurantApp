using RestaurantApp.ViewModels;

namespace RestaurantApp.Views;

public partial class DrinksPage : ContentPage
{
	public DrinksPage(DrinksViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
}