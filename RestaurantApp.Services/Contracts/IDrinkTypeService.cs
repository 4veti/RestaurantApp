using RestaurantApp.Domain.Contracts.DTOs;

namespace RestaurantApp.Services.Contracts;

public interface IDrinkTypeService
{
    Task<IEnumerable<DrinkTypeDto>> GetAllAsync();
    Task<DrinkTypeDto?> GetByIdAsync(int id);
    Task<string> AddAsync(DrinkTypeDto dto);
    Task<string?> UpdateAsync(DrinkTypeDto dto);
    Task<string?> DeleteByIdAsync(int id);
}
