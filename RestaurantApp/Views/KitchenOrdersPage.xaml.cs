using RestaurantApp.ViewModels;

namespace RestaurantApp.Views;

public partial class KitchenOrdersPage : ContentPage
{
    private IDispatcherTimer _getOrdersTimer;
    private KitchenOrdersPageViewModel _viewModel;

    public KitchenOrdersPage(KitchenOrdersPageViewModel viewModel)
    {
        BindingContext = viewModel;
        _viewModel = viewModel;
        InitializeComponent();

        _viewModel.UpdateElapsedTimes();
    }

    protected override async void OnAppearing()
    {
        if (_getOrdersTimer == null)
        {
            _getOrdersTimer = Dispatcher.CreateTimer();
            _getOrdersTimer.Interval = TimeSpan.FromSeconds(10);
            _getOrdersTimer.Tick += async (_, _) => { await OrdersTimerTickLogic(); };
            _getOrdersTimer.Start();

            await OrdersTimerTickLogic();
        }
        else if (_getOrdersTimer.IsRunning == false)
        {
            _getOrdersTimer.Start();
        }

        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        _getOrdersTimer?.Stop();
        base.OnDisappearing();
    }

    private async Task OrdersTimerTickLogic()
    {
        bool anyNewOrders = await _viewModel.AnyNewOrders();

        if (anyNewOrders)
        {
            await _viewModel.GetNewOrders();
        }

        _viewModel.UpdateElapsedTimes();
        FramePendingOrders.IsVisible = _viewModel.PendingOrders.Any();
    }

    private void ButtonMarkOrderAsActive_Clicked(object sender, EventArgs e)
    {
        FrameActiveOrders.IsVisible = true;
        if (_viewModel.PendingOrders.Any())
        {
            return;
        }

        FramePendingOrders.IsVisible = false;
    }
}