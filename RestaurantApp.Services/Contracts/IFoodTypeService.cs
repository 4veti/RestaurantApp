using RestaurantApp.Domain.Contracts.DTOs;

namespace RestaurantApp.Services.Contracts;

public interface IFoodTypeService
{
    Task<IEnumerable<FoodTypeDto>> GetAllAsync();
    Task<FoodTypeDto?> GetByIdAsync(int id);
    Task<string> AddAsync(FoodTypeDto dto);
    Task<string?> UpdateAsync(FoodTypeDto dto);
    Task<string?> DeleteByIdAsync(int id);
}
