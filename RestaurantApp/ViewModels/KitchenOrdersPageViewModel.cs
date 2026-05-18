using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Client.Models;
using RestaurantApp.Services;
using Microsoft.Maui.Animations;

namespace RestaurantApp.ViewModels;

public partial class KitchenOrdersPageViewModel : ObservableObject
{
    private readonly RestaurantService _service;
    private int _lastOrderId = 0;

    public KitchenOrdersPageViewModel(RestaurantService service)
    {
        _service = service;
    }

    public ObservableCollection<ExtendedOrderDto> ActiveOrders { get; set; } = new();
    public ObservableCollection<ExtendedOrderDto> PendingOrders { get; set; } = new();

    public async Task GetNewOrders()
    {
        List<OrderDto> newOrders = await _service.GetNewOrdersForKitchen(_lastOrderId);

        if (!newOrders.Any())
        {
            return;
        }

        foreach (OrderDto order in newOrders)
        {
            PendingOrders.Add(new ExtendedOrderDto(order));
        }

        _lastOrderId = PendingOrders.Max(o => o.Id);
    }

    public void UpdateElapsedTimes()
    {
        for (int i = 0; i < PendingOrders.Count; i++)
        {
            PendingOrders[i].ElapsedMinutes = (int)(DateTime.Now - PendingOrders[i].Created).TotalMinutes;
            PendingOrders[i] = new ExtendedOrderDto(PendingOrders[i]);
        }

        for (int i = 0; i < ActiveOrders.Count; i++)
        {
            ActiveOrders[i].ElapsedMinutes = (int)(DateTime.Now - ActiveOrders[i].Created).TotalMinutes;
            ActiveOrders[i] = new ExtendedOrderDto(PendingOrders[i]);
        }
    }

    public async Task<bool> AnyNewOrders()
    {
        bool anyNewOrders = await _service.AnyNewOrders(_lastOrderId);

        return anyNewOrders;
    }

    [RelayCommand]
    private async Task MarkAsServed(ExtendedOrderDto orderToMark)
    {
        bool success = await _service.MarkOrderAsServed(orderToMark.Id);

        if (success)
        {
            ActiveOrders.Remove(orderToMark);
        }
    }

    [RelayCommand]
    private void MarkAsActive(ExtendedOrderDto orderToMark)
    {
        PendingOrders.Remove(orderToMark);
        ActiveOrders.Add(orderToMark);
    }
}