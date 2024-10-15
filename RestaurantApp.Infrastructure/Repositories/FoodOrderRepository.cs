using RestaurantApp.Domain.Contracts;
using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Infrastructure.Repositories;

public class FoodOrderRepository : IFoodOrderRepository
{
    public Task<IQueryable<FoodOrder>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FoodOrder> GetByIdAsync(int orderId, int foodId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(IEnumerable<FoodOrder> order)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(IEnumerable<FoodOrder> order)
    {
        throw new NotImplementedException();
    }
}
