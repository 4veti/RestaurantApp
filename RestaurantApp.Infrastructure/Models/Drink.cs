using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RestaurantApp.Infrastructure.Constants;

namespace RestaurantApp.Infrastructure.Models;

public class Drink
{
    public int Id { get; set; }

    [Required]
    [MaxLength(MaxDrinkNameLength)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public int DrinkTypeId { get; set; }

    [ForeignKey(nameof(DrinkTypeId))]
    public DrinkType DrinkType { get; set; } = null!;

    public int Millimetres { get; set; }

    public bool IsAlcoholic { get; set; }

    public double? AlcoholPercentage { get; set; }
}
