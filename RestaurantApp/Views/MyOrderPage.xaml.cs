using CommunityToolkit.Mvvm.Input;
using RestaurantApp.ViewModels;

namespace RestaurantApp.Views;

public partial class MyOrderPage : ContentPage
{
	public MyOrderPage(MyOrderViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
		this.FlowDirection = FlowDirection.RightToLeft;
		(BindingContext as MyOrderViewModel)?.LoadOrderItems();
        base.OnNavigatedTo(args);
    }
}