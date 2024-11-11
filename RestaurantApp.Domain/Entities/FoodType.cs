namespace RestaurantApp.Domain.Entities;

using System.ComponentModel.DataAnnotations;
using static RestaurantApp.Domain.Constants;

public class FoodType
{
    [Key]
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
