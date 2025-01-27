namespace RestaurantApp.Domain.Contracts.DTOs;

public class MenuDto
{
    public IEnumerable<FoodDto> Foods { get; set; } = new List<FoodDto>();
    public IEnumerable<DrinkDto> Drinks { get; set; } = new List<DrinkDto>();
    public IEnumerable<FoodTypeDto> FoodTypes { get; set; } = new List<FoodTypeDto>();
    public IEnumerable<DrinkTypeDto> DrinkTypes { get; set; } = new List<DrinkTypeDto>();
}
