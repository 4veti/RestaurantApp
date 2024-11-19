using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain.Contracts.Repositories;
using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Infrastructure.Repositories;

public class DrinkOrderRepository : IDrinkOrderRepository
{
    private readonly RestaurantAppDbContext _context;

    public DrinkOrderRepository(RestaurantAppDbContext context)
    {
        _context = context;
    }

    public IQueryable<DrinkOrder> GetAllAsync(bool asNoTracking = false)
    {
        IQueryable<DrinkOrder> orders = _context.Set<DrinkOrder>();

        if (asNoTracking)
        {
            orders = orders.AsNoTracking();
        }

        return orders;
    }

    public async Task<DrinkOrder?> GetByIdAsync(int orderId, int foodId)
        => await _context.FindAsync<DrinkOrder>(orderId, foodId);

    public async Task InsertRangeAsync(IEnumerable<DrinkOrder> drinksOrders)
        => await _context.AddRangeAsync(drinksOrders);

    public void RemoveAllByOrderId(int orderId)
        => _context.RemoveRange(
                _context.Set<DrinkOrder>()
                .Where(fo => fo.OrderId == orderId));

    public void RemoveRangeAsync(IEnumerable<DrinkOrder> drinksOrders)
        => _context.RemoveRange(drinksOrders);
}
