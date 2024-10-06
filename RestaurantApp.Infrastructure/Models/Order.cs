using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Infrastructure.Models;

public class Order
{
    public int Id { get; set; }

    [Required]
    public DateTime CreatedOn { get; set; }

    public DateTime? ServedOn { get; set; }

    [Required]
    public string OrderName { get; set; } = string.Empty;

    public bool IsPaid { get; set; } = false;

    public bool IsServed { get; set; } = false;

    public IEnumerable<FoodOrder> FoodsOrders { get; set; } = new List<FoodOrder>();
}
