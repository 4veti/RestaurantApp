using System.ComponentModel.DataAnnotations;
using static RestaurantApp.Domain.Constants;

namespace RestaurantApp.Domain.Contracts.DTOs;

public class FoodTypeDto
{
    public int Id { get; set; }

    [StringLength(FoodNameMaxLength,
        MinimumLength = FoodNameMinLength)]
    public string Name { get; set; } = string.Empty;
}
