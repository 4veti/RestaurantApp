namespace RestaurantApp.Domain.Contracts.Repositories;

using RestaurantApp.Domain.Entities;

public interface IOrderRepository
{
    IQueryable<Order> GetAllAsync(bool asNoTracking = false);
    Task<Order?> GetByIdAsync(int id);
    Task InsertAsync(Order order);
    void Update(Order order);
    void Remove(Order order);
}
