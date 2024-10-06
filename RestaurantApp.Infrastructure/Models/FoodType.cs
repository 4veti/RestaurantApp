using System.ComponentModel.DataAnnotations;
using static RestaurantApp.Infrastructure.Constants;

namespace RestaurantApp.Infrastructure.Models;

public class FoodType
{
    public int Id { get; set; }

    [Required]
    [MaxLength(FoodNameMaxLength)]
    public string Name { get; set; } = string.Empty;

    public IEnumerable<Food> Foods { get; set; } = new List<Food>();
}
