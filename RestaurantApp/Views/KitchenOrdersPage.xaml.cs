using System.Runtime.CompilerServices;
using RestaurantApp.Client.Models;
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

        _getOrdersTimer = Dispatcher.CreateTimer();
        _getOrdersTimer.Interval = TimeSpan.FromSeconds(10);
        _getOrdersTimer.Tick += OrdersTimerTickLogic;
        _getOrdersTimer.Start();

        _viewModel.UpdateElapsedTimes();
    }

    protected override void OnAppearing()
    {
        if (_getOrdersTimer == null)
        {
            _getOrdersTimer = Dispatcher.CreateTimer();
            _getOrdersTimer.Interval = TimeSpan.FromSeconds(10);
            _getOrdersTimer.Tick += OrdersTimerTickLogic;
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

    private async void OrdersTimerTickLogic(object? sender, EventArgs e)
    {
        bool? anyNewOrders = await _viewModel.AnyNewOrders();

        if (anyNewOrders is null || anyNewOrders == false)
        {
            _viewModel.UpdateElapsedTimes();
            return;
        }

        await _viewModel.GetNewOrders();
        FramePendingOrders.IsVisible = true;
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