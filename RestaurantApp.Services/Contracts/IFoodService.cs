using RestaurantApp.Domain.Contracts.DTOs;

namespace RestaurantApp.Services.Contracts;

public interface IFoodService
{
    Task<IEnumerable<FoodDto>> GetAllByFoodTypeIdAsync(int foodTypeId);
    Task<FoodDto?> GetByIdAsync(int id);
    Task<string> AddAsync(FoodDto foodDto);
    Task<string?> UpdateAsync(FoodDto foodDto);
    Task<string?> DeleteByIdAsync(int id);
}