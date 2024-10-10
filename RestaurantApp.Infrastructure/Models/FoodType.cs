using System.ComponentModel.DataAnnotations;
using static RestaurantApp.Infrastructure.Constants;

namespace RestaurantApp.Infrastructure.Models;

public class FoodType
{
    public int Id { get; set; }

    [Required]
    [MaxLength(FoodNameMaxLength)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime Created { get; set; }

    [Required]
    public DateTime Modified { get; set; }

    public IEnumerable<Food> Foods { get; set; } = new List<Food>();
}
