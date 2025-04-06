using System.ComponentModel.DataAnnotations;
using static RestaurantApp.Domain.Constants;

namespace RestaurantApp.Domain.Contracts.DTOs;

public class DrinkDto
{
    public int Id { get; set; }

    [StringLength(MaxDrinkNameLength,
        MinimumLength = MinDrinkNameLength)]
    public string Name { get; set; } = string.Empty;

    [Range(MinPrice, MaxPrice)]
    public decimal Price { get; set; }

    public int DrinkTypeId { get; set; }

    public string DrinkType { get; set; } = string.Empty;

    [Range(MinMillilitres, MaxMillilitres)]
    public int Millilitres { get; set; }

    public bool IsAlcoholic { get; set; }

    [Range(MinAlcoholPercentage, MaxAlcoholPercentage)]
    public double? AlcoholPercentage { get; set; }

    public int Count { get; set; } = 1;
}
