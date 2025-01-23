using RestaurantApp.Domain.Contracts.DTOs;

namespace RestaurantApp.Services.Contracts;

public interface IDrinkService
{
    Task<IEnumerable<DrinkDto>> GetAllByDrinkTypeIdAsync(int drinkType);
    Task<DrinkDto?> GetByIdAsync(int id);
    Task<string> AddAsync(DrinkDto dto);
    Task<string?> UpdateAsync(int id, DrinkDto dto);
    Task<bool?> DeleteByIdAsync(int id);
}
