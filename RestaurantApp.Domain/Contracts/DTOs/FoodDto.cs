using System.ComponentModel.DataAnnotations;
using static RestaurantApp.Domain.Constants;

namespace RestaurantApp.Domain.Contracts.DTOs;

public class FoodDto
{
    public int Id { get; set; }

    [StringLength(FoodNameMaxLength,
        MinimumLength = FoodNameMinLength)]
    public string Name { get; set; } = string.Empty;

    [Range(NetGramsMin, NetGramsMax)]
    public int NetGrams { get; set; }

    [Range(MinPrice, MaxPrice)]
    public decimal Price { get; set; }

    public int FoodTypeId { get; set; }

    public string FoodType { get; set; } = string.Empty!;

    public int Count { get; set; }
}
