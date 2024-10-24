using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Domain.Contracts.Repositories;

public interface IFoodRepository
{
    IQueryable<Food> GetAllAsync(bool asNoTracking = false);
    Task<Food?> GetByIdAsync(int id);
    Task InsertAsync(Food food);
    void Update(Food food);
    void Remove(Food food);
}
