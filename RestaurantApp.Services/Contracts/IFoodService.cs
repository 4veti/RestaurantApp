using RestaurantApp.Domain.Contracts.DTOs;

namespace RestaurantApp.Services.Contracts;

public interface IFoodService
{
    Task<IEnumerable<FoodDto>> GetAllByFoodTypeIdAsync(int foodTypeId);
    Task<FoodDto> GetByIdAsync(int id);
    Task AddAsync(FoodDto foodDto);
    Task UpdateAsync(int id, FoodDto foodDto);
    Task DeleteByIdAsync(int id);
}