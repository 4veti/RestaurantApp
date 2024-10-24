using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain.Contracts.Repositories;
using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Infrastructure.Repositories;

public sealed class FoodTypeRepository : IFoodTypeRepository
{
    private readonly RestaurantAppDbContext _context;

    public FoodTypeRepository(RestaurantAppDbContext context)
    {
        _context = context;
    }

    public IQueryable<FoodType> GetAllAsync(bool asNoTracking = false)
    {
        IQueryable<FoodType> orders = _context.Set<FoodType>();

        if (asNoTracking)
        {
            orders = orders.AsNoTracking();
        }

        return orders;
    }

    public async Task<FoodType?> GetByIdAsync(int id)
        => await _context.FindAsync<FoodType>(id);

    public void Update(FoodType foodType)
        => _context.Update(foodType);

    public async Task InsertAsync(FoodType foodType)
        => await _context.AddAsync(foodType);

    public void Remove(FoodType foodType)
        => _context.Remove(foodType);
}
