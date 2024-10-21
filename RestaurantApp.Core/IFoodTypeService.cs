using RestaurantApp.Domain.Contracts.DTOs;

namespace RestaurantApp.Services.Abstractions;

public interface IFoodTypeService
{
    Task<IEnumerable<FoodTypeDto>> GetAllAsync();
    Task<FoodTypeDto> GetByIdAsync(int id);
    Task AddAsync(FoodTypeDto dto);
    Task UpdateAsync(int id, FoodTypeDto dto);
    Task DeleteByIdAsync(int id);
}
