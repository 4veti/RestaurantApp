using System.ComponentModel.DataAnnotations;
using static RestaurantApp.Domain.Constants;

namespace RestaurantApp.Domain.Contracts.DTOs;

public class OrderDto
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Completed { get; set; }

    [StringLength(OrderNameMaxLength,
        MinimumLength = OrderNameMinLength)]
    public string OrderName { get; set; } = string.Empty;

    public bool IsPaid { get; set; } = false;

    public bool IsServed { get; set; } = false;
}
