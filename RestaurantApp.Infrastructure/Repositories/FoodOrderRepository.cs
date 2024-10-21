using RestaurantApp.Domain.Contracts.Repositories;
using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Infrastructure.Repositories;

public class FoodOrderRepository : IFoodOrderRepository
{
    private readonly RestaurantAppDbContext _context;

    public FoodOrderRepository(RestaurantAppDbContext context)
    {
        _context = context;
    }

    public Task<IQueryable<FoodOrder>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<FoodOrder> GetByIdAsync(int orderId, int foodId)
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
