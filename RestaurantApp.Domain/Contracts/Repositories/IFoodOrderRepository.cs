using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Domain.Contracts.Repositories;

public interface IFoodOrderRepository
{
    Task<IQueryable<FoodOrder>> GetAllAsync();
    Task<FoodOrder> GetByIdAsync(int orderId, int foodId);
    Task InsertAsync(IEnumerable<FoodOrder> order);
    Task RemoveAsync(IEnumerable<FoodOrder> order);
}
