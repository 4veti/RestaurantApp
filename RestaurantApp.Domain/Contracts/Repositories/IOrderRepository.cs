namespace RestaurantApp.Domain.Contracts.Repositories;
using RestaurantApp.Domain.Entities;

public interface IOrderRepository
{
    Task<IQueryable<Order>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Order> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task InsertAsync(Order order);
    Task EditAsync(Order order);
    Task RemoveAsync(Order order);
}
