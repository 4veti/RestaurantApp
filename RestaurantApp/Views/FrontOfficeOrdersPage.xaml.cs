using RestaurantApp.ViewModels;

namespace RestaurantApp.Views;

public partial class FrontOfficeOrdersPage : ContentPage
{
    private IDispatcherTimer _getOrdersTimer;
	private FrontOfficeViewModel _viewModel;

	public FrontOfficeOrdersPage(FrontOfficeViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
		BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_getOrdersTimer == null)
        {
            _getOrdersTimer = Dispatcher.CreateTimer();
            _getOrdersTimer.Tick += async (_, _) => { await OrdersTimerTickLogic(); };
            _getOrdersTimer.Interval = TimeSpan.FromSeconds(10);
            _getOrdersTimer.Start();

            await OrdersTimerTickLogic();
        }
        else if (_getOrdersTimer.IsRunning == false)
        {
            _getOrdersTimer.Start();
        }
    }

    private async Task OrdersTimerTickLogic()
    {
        await _viewModel.GetNewOrders();
    }
}