using RestaurantApp.Domain.Contracts.DTOs;

namespace RestaurantApp.Services.Contracts;

public interface IFoodService
{
    Task<IEnumerable<FoodDto>> GetAllByFoodTypeIdAsync(int foodTypeId);
    Task<FoodDto?> GetByIdAsync(int id);
    Task<string> AddAsync(FoodDto foodDto);
    Task<bool?> UpdateAsync(int id, FoodDto foodDto);
    Task<bool?> DeleteByIdAsync(int id);
}