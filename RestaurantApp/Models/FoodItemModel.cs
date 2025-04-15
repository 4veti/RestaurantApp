using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using RestaurantApp.Domain.Contracts.DTOs;

namespace RestaurantApp.Models;

public partial class FoodItemModel : ObservableObject
{
    public FoodItemModel(FoodDto dto)
    {
        Id = dto.Id;
        Name = dto.Name;
        NetGrams = dto.NetGrams;
        Price = dto.Price;
        FoodTypeId = dto.FoodTypeId;
        FoodTypeName = dto.FoodTypeName;
        count = dto.Count;
    }

    [ObservableProperty]
    private int count;

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int NetGrams { get; set; }

    public decimal Price { get; set; }

    public int FoodTypeId { get; set; }

    public string? FoodTypeName { get; set; }

}
