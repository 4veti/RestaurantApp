using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Services;

namespace RestaurantApp.ViewModels;

public partial class KitchenOrdersPageViewModel : ObservableObject
{
    private readonly RestaurantService _service;
    private int _lastOrderId = 1;

    public KitchenOrdersPageViewModel(RestaurantService service)
    {
        _service = service;
    }

    public ObservableCollection<OrderDto> ActiveOrders { get; set; } = new();
    public ObservableCollection<OrderDto> PendingOrders { get; set; } = new();

    public async Task GetNewOrders()
    {
        List<OrderDto> newOrders = await _service.GetNewOrders(_lastOrderId);

        if (!newOrders.Any())
        {
            return;
        }

        foreach (OrderDto order in newOrders)
        {
            PendingOrders.Add(order);
        }

        _lastOrderId = PendingOrders.Max(o => o.Id);
    }

    public async Task<bool?> AnyNewOrders()
    {
        bool? anyNewOrders = await _service.AnyNewOrders(_lastOrderId);

        return anyNewOrders;
    }

    [RelayCommand]
    private async Task MarkAsServed(OrderDto orderToMark)
    {
        bool success = await _service.MarkOrderAsServed(orderToMark.Id);

        if (success)
        {
            ActiveOrders.Remove(orderToMark);
        }
    }

    [RelayCommand]
    private void MarkAsActive(OrderDto orderToMark)
    {
        PendingOrders.Remove(orderToMark);
        ActiveOrders.Add(orderToMark);
    }
}