namespace RestaurantApp.Domain.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class FoodOrder
{
    public int FoodId { get; set; }

    [ForeignKey(nameof(FoodId))]
    public Food Food { get; set; } = null!;

    public int OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; } = null!;

    [Required]
    public DateTime Created { get; set; }

    [Required]
    public int Count { get; set; }
}
