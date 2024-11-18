using System.ComponentModel.DataAnnotations;
using static RestaurantApp.Domain.Constants;

namespace RestaurantApp.Domain.Contracts.DTOs;

public class DrinkTypeDto
{
    public int Id { get; set; }

    [StringLength(MaxDrinkNameLength,
        MinimumLength = MinDrinkNameLength)]
    public string Name { get; set; } = string.Empty;
}
