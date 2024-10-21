using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Domain.Contracts.Repositories;

public interface IFoodTypeRepository
{
    Task<IQueryable<FoodType>> GetAllAsync();
    Task<FoodType> GetByIdAsync(int id);
    Task InsertAsync(FoodType order);
    Task EditAsync(FoodType order);
    Task RemoveAsync(FoodType order);
}
