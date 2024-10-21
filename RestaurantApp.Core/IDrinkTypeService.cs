using RestaurantApp.Domain.Contracts.DTOs;

namespace RestaurantApp.Services.Abstractions;

public interface IDrinkTypeService
{
    Task<IEnumerable<DrinkTypeDto>> GetAllAsync();
    Task<DrinkTypeDto> GetByIdAsync(int id);
    Task AddAsync(DrinkTypeDto dto);
    Task UpdateAsync(int id, DrinkTypeDto dto);
    Task DeleteByIdAsync(int id);
}
