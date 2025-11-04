using RestaurantApp.ViewModels;

namespace RestaurantApp.Views;

public partial class FrontOfficeOrdersPage : ContentPage
{
	public FrontOfficeOrdersPage(FrontOfficeViewModel frontOfficeViewModel)
	{
		InitializeComponent();
		BindingContext = frontOfficeViewModel;
	}
}