using System.Runtime.CompilerServices;
using RestaurantApp.ViewModels;

namespace RestaurantApp.Views;

public partial class KitchenOrdersPage : ContentPage
{
	private IDispatcherTimer _timer;
    private KitchenOrdersPageViewModel _viewModel;

	public KitchenOrdersPage(KitchenOrdersPageViewModel viewModel)
	{
		BindingContext = viewModel;
        _viewModel = viewModel;
		InitializeComponent();

        _timer = Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(10);
        _timer.Tick += OrdersTimerTickLogic;
        _timer.Start();
    }

    protected override void OnAppearing()
    {
        if (_timer == null)
        {
            _timer = Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromSeconds(10);
            _timer.Tick += OrdersTimerTickLogic;
        }
        else if (_timer.IsRunning == false)
        {
            _timer.Start();
        }

        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        _timer?.Stop();
        base.OnDisappearing();
    }

    private async void OrdersTimerTickLogic(object? sender, EventArgs e)
    {
        bool? anyNewOrders = await _viewModel.AnyNewOrders();

        if (anyNewOrders is null || anyNewOrders == false)
        {
            return;
        }

        await _viewModel.GetNewOrders();
    }
}