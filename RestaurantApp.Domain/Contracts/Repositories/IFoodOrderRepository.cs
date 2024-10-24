using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Domain.Contracts.Repositories;

public interface IFoodOrderRepository
{
    IQueryable<FoodOrder> GetAllAsync(bool asNoTracking = false);
    Task<FoodOrder?> GetByIdAsync(int orderId, int foodId);
    Task InsertRangeAsync(IEnumerable<FoodOrder> order);
    void RemoveRangeAsync(IEnumerable<FoodOrder> order);
    void RemoveAllByOrderId(int orderId);
}
