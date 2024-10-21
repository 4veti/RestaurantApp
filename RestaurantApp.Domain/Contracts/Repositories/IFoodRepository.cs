using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Domain.Contracts.Repositories;

public interface IFoodRepository
{
    Task<IQueryable<Food>> GetAllAsync();
    Task<Food> GetByIdAsync(int id);
    Task InsertAsync(Food order);
    Task EditAsync(Food order);
    Task RemoveAsync(Food order);
}
