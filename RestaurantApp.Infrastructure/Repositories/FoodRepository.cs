using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain.Contracts.Repositories;
using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Infrastructure.Repositories;

public sealed class FoodRepository : IFoodRepository
{
    private readonly RestaurantAppDbContext _context;

    public FoodRepository(RestaurantAppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Food> GetAll(bool asNoTracking = false)
    {
        IQueryable<Food> orders = _context.Set<Food>();

        if (asNoTracking)
        {
            orders = orders.AsNoTracking();
        }

        return orders;
    }

    public async Task<Food?> GetByIdAsync(int id)
        => await _context.FindAsync<Food>(id);

    public void Update(Food food)
        => _context.Update(food);

    public async Task InsertAsync(Food food)
        => await _context.AddAsync(food);

    public void Remove(Food food)
        => _context.Remove(food);
}
