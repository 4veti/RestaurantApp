using RestaurantApp.Domain.Contracts.DTOs;

namespace RestaurantApp.Services.Contracts;

public interface IDrinkTypeService
{
    Task<IEnumerable<DrinkTypeDto>> GetAllAsync();
    Task<DrinkTypeDto?> GetByIdAsync(int id);
    Task AddAsync(DrinkTypeDto dto);
    Task<bool> UpdateAsync(int id, DrinkTypeDto dto);
    Task<bool> DeleteByIdAsync(int id);
}
