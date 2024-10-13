namespace RestaurantApp.Domain.Entities;

using System.ComponentModel.DataAnnotations;
using static RestaurantApp.Domain.Constants;

public class DrinkType
{
    public int Id { get; set; }

    [MaxLength(MaxDrinkTypeNameLength)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime Created { get; set; }

    [Required]
    public DateTime Modified { get; set; }

    public IEnumerable<Drink> Drinks { get; set; } = new List<Drink>();
}
