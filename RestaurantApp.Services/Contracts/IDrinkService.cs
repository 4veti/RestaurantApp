using RestaurantApp.Domain.Contracts.DTOs;

namespace RestaurantApp.Services.Contracts;

public interface IDrinkService
{
    Task<IEnumerable<DrinkDto>> GetAllByDrinkTypeIdAsync(int drinkType);
    Task<DrinkDto> GetByIdAsync(int id);
    Task AddAsync(DrinkDto dto);
    Task UpdateAsync(int id, DrinkDto dto);
    Task DeleteByIdAsync(int id);
}
