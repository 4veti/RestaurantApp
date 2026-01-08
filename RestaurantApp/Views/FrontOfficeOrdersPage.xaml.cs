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
        bool? anyNewOrders = await _viewModel.AnyNewOrders();

        if (anyNewOrders is null || anyNewOrders == false)
        {
            return;
        }

        await _viewModel.GetNewOrders();
        //FramePendingOrders.IsVisible = true;
    }
}