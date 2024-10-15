using RestaurantApp.Contracts;

namespace RestaurantApp.Services.Abstractions;

public interface IFoodTypeService
{
    Task<IEnumerable<FoodTypeDto>> GetAllAsync();
    Task<FoodTypeDto> GetByIdAsync(int id);
    Task AddAsync(FoodTypeDto dto);
    Task UpdateAsync(int id, FoodTypeDto dto);
    Task DeleteByIdAsync(int id);
}
