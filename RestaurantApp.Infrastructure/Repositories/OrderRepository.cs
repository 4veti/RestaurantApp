using RestaurantApp.Domain.Contracts.Repositories;
using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Infrastructure.Repositories;

public sealed class OrderRepository : IOrderRepository
{
    private readonly RestaurantAppDbContext _context;

    public OrderRepository(RestaurantAppDbContext context)
    {
        _context = context;
    }

    public Task EditAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<Order>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Order order)
    {
        throw new NotImplementedException();
    }
}
