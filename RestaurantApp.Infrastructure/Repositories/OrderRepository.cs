using RestaurantApp.Domain.Contracts.Repositories;
using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Infrastructure.Repositories;

public sealed class OrderRepository : IOrderRepository
{
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
