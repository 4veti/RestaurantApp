namespace RestaurantApp.Domain.Entities;
using static RestaurantApp.Domain.Constants;

using System.ComponentModel.DataAnnotations;

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime Created { get; set; }

    public DateTime? Completed { get; set; }

    public DateTime Modified { get; set; }

    [Required]
    [MaxLength(OrderNameMaxLength)]
    public string OrderName { get; set; } = string.Empty;

    public bool IsPaid { get; set; } = false;

    public bool IsServed { get; set; } = false;

    public IEnumerable<FoodOrder> FoodsOrders { get; set; } = new List<FoodOrder>();
}
