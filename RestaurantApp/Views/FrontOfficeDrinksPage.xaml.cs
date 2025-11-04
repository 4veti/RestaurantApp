using RestaurantApp.ViewModels;

namespace RestaurantApp.Views;

public partial class FrontOfficeDrinksPage : ContentPage
{
	public FrontOfficeDrinksPage(FrontOfficeViewModel frontOfficeViewModel)
	{
		InitializeComponent();
		BindingContext = frontOfficeViewModel;
	}
}