using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Domain.Entities;

public class DrinkOrder
{
    public int DrinkId { get; set; }

    [ForeignKey(nameof(DrinkId))]
    public Drink Drink { get; set; } = null!;

    public int OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; } = null!;

    [Required]
    public DateTime Created { get; set; }

    [Required]
    public int Count { get; set; }
}
