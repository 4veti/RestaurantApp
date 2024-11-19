using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Domain.Contracts.Repositories;

public interface IDrinkOrderRepository
{
    IQueryable<DrinkOrder> GetAllAsync(bool asNoTracking = false);
    Task<DrinkOrder?> GetByIdAsync(int orderId, int foodId);
    Task InsertRangeAsync(IEnumerable<DrinkOrder> order);
    void RemoveRangeAsync(IEnumerable<DrinkOrder> order);
    void RemoveAllByOrderId(int orderId);
}
