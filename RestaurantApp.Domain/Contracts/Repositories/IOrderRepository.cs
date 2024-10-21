namespace RestaurantApp.Domain.Contracts.Repositories;
using RestaurantApp.Domain.Entities;

public interface IOrderRepository
{
    Task<IQueryable<Order>> GetAllAsync();
    Task<Order> GetByIdAsync(int id);
    Task InsertAsync(Order order);
    Task EditAsync(Order order);
    Task RemoveAsync(Order order);
}
