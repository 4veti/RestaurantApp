using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Domain.Contracts.Repositories;

public interface IFoodOrderRepository
{
    Task<IQueryable<FoodOrder>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<FoodOrder> GetByIdAsync(int orderId, int foodId, CancellationToken cancellationToken = default);
    Task InsertAsync(IEnumerable<FoodOrder> order);
    Task RemoveAsync(IEnumerable<FoodOrder> order);
}
