using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantApp.Infrastructure.Models;

public class FoodOrder
{
    public int FoodId { get; set; }

    [ForeignKey(nameof(FoodId))]
    public Food Food { get; set; } = null!;

    public int OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; } = null!;
}
