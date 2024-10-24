using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Domain.Contracts.Repositories;

public interface IFoodTypeRepository
{
    IQueryable<FoodType> GetAllAsync(bool asNoTracking = false);
    Task<FoodType?> GetByIdAsync(int id);
    Task InsertAsync(FoodType foodType);
    void Update(FoodType foodType);
    void Remove(FoodType foodType);
}
