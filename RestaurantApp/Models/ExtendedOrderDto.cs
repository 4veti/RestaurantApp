using CommunityToolkit.Mvvm.ComponentModel;
using RestaurantApp.Domain.Contracts.DTOs;

namespace RestaurantApp.Client.Models;

public partial class ExtendedOrderDto : ObservableObject
{
    public ExtendedOrderDto()
    {
        
    }

    public ExtendedOrderDto(OrderDto order)
    {
        Id = order.Id;
        Created = order.Created;
        Completed = order.Completed;
        OrderName = order.OrderName;
        IsPaid = order.IsPaid;
        IsServed = order.IsServed;
        Foods = order.Foods;
        Drinks = order.Drinks;
    }

    public ExtendedOrderDto(ExtendedOrderDto order)
    {
        Id = order.Id;
        Created = order.Created;
        Completed = order.Completed;
        OrderName = order.OrderName;
        IsPaid = order.IsPaid;
        IsServed = order.IsServed;
        Foods = order.Foods;
        Drinks = order.Drinks;
        elapsedMinutes = order.elapsedMinutes;
    }

    public int Id { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Completed { get; set; }

    public string OrderName { get; set; } = string.Empty;

    public bool IsPaid { get; set; } = false;

    [ObservableProperty]
    public bool isServed;

    public ICollection<FoodDto> Foods { get; set; } = new List<FoodDto>();
    public ICollection<DrinkDto> Drinks { get; set; } = new List<DrinkDto>();
    [ObservableProperty]
    public int elapsedMinutes;
}
