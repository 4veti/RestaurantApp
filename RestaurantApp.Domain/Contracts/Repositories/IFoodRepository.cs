using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Domain.Contracts.Repositories;

public interface IFoodRepository
{
    Task<IQueryable<Food>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Food> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task InsertAsync(Food order);
    Task EditAsync(Food order);
    Task RemoveAsync(Food order);
}
