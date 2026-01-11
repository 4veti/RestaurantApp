using RestaurantApp.ViewModels;

namespace RestaurantApp.Views;

public partial class FrontOfficeOrdersPage : ContentPage
{
    private IDispatcherTimer _getOrdersTimer;
	private FrontOfficeViewModel _viewModel;

	public FrontOfficeOrdersPage(FrontOfficeViewModel viewModel)
	{
        _viewModel = viewModel;
		BindingContext = viewModel;
		InitializeComponent();

        _getOrdersTimer = Dispatcher.CreateTimer();
        _getOrdersTimer.Interval = TimeSpan.FromSeconds(10);
        _getOrdersTimer.Tick += OrdersTimerTickLogic;
        _getOrdersTimer.Start();
    }

    private async void OrdersTimerTickLogic(object? sender, EventArgs e)
    {
        await _viewModel.GetNewOrders();
    }
}