using CommunityToolkit.Mvvm.ComponentModel;
using RestaurantApp.Domain.Contracts.DTOs;

namespace RestaurantApp.Models;

public partial class DrinkItemModel : ObservableObject
{
    [ObservableProperty]
    private int count;

    public DrinkItemModel(DrinkDto dto)
    {
        Id = dto.Id;
        Name = dto.Name;
        Millilitres = dto.Millilitres;
        Price = dto.Price;
        DrinkTypeId = dto.DrinkTypeId;
        DrinkType = dto.DrinkType;
        count = dto.Count;
        IsAlcoholic = dto.IsAlcoholic;
        AlcoholPercentage = dto.AlcoholPercentage;
    }

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int DrinkTypeId { get; set; }

    public string DrinkType { get; set; } = string.Empty;

    public int Millilitres { get; set; }

    public bool IsAlcoholic { get; set; }

    public double? AlcoholPercentage { get; set; }
}
