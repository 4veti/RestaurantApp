using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using RestaurantApp.Domain.Contracts.DTOs;

namespace RestaurantApp.Client.Models;

public class ExtendedOrderDto
{
    private int _elapsedMinutes;

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

    public int Id { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Completed { get; set; }

    public string OrderName { get; set; } = string.Empty;

    public bool IsPaid { get; set; } = false;

    public bool IsServed { get; set; } = false;

    public ICollection<FoodDto> Foods { get; set; } = new List<FoodDto>();
    public ICollection<DrinkDto> Drinks { get; set; } = new List<DrinkDto>();
    public int ElapsedMinutes { get; set; }
}
