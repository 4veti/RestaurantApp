using RestaurantApp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static RestaurantApp.Domain.Constants;

namespace RestaurantApp.Domain.Contracts.DTOs;

public class DrinkDto
{
    [StringLength(MaxDrinkNameLength,
        MinimumLength = MinDrinkNameLength)]
    public string Name { get; set; } = string.Empty;

    [Range(MinPrice, MaxPrice)]
    public decimal Price { get; set; }

    public int DrinkTypeId { get; set; }

    public string DrinkType { get; set; } = null!;

    [Range(MinMillilitres, MaxMillilitres)]
    public int Millilitres { get; set; }

    public bool IsAlcoholic { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    [Range(MinAlcoholPercentage, MaxAlcoholPercentage)]
    public double? AlcoholPercentage { get; set; }
}
