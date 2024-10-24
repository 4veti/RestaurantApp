using Microsoft.EntityFrameworkCore;
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

    public IQueryable<Order> GetAllAsync(bool asNoTracking = false)
    {
        IQueryable<Order> orders = _context.Set<Order>();

        if (asNoTracking)
        {
            orders = orders.AsNoTracking();
        }

        return orders;
    }

    public async Task<Order?> GetByIdAsync(int id)
        => await _context.FindAsync<Order>(id);

    public void Update(Order order)
        =>_context.Update(order);

    public async Task InsertAsync(Order order)
        => await _context.AddAsync(order);

    public void Remove(Order order)
        => _context.Remove(order);
}
