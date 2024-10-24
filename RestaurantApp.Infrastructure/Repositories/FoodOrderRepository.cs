using Microsoft.EntityFrameworkCore;
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

    public IQueryable<FoodOrder> GetAllAsync(bool asNoTracking = false)
    {
        IQueryable<FoodOrder> orders = _context.Set<FoodOrder>();

        if (asNoTracking)
        {
            orders = orders.AsNoTracking();
        }

        return orders;
    }

    public async Task<FoodOrder?> GetByIdAsync(int orderId, int foodId)
        => await _context.FindAsync<FoodOrder>(orderId, foodId);

    public async Task InsertRangeAsync(IEnumerable<FoodOrder> foodOrders)
        => await _context.AddRangeAsync(foodOrders);

    public void RemoveAllByOrderId(int orderId)
        => _context.RemoveRange(
                _context.Set<FoodOrder>()
                .Where(fo => fo.OrderId == orderId));

    public void RemoveRangeAsync(IEnumerable<FoodOrder> foodOrders)
        => _context.RemoveRange(foodOrders);
}
