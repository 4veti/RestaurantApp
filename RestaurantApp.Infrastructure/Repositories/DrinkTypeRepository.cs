using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain.Contracts.Repositories;
using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Infrastructure.Repositories;

public sealed class DrinkTypeRepository : IDrinkTypeRepository
{
    private readonly RestaurantAppDbContext _context;

    public DrinkTypeRepository(RestaurantAppDbContext context)
    {
        _context = context;
    }

    public IQueryable<DrinkType> GetAllAsync(bool asNoTracking = false)
    {
        IQueryable<DrinkType> drinkTypes = _context.Set<DrinkType>();

        if (asNoTracking)
        {
            drinkTypes = drinkTypes.AsNoTracking();
        }

        return drinkTypes;
    }

    public async Task<DrinkType?> GetByIdAsync(int id)
        => await _context.FindAsync<DrinkType>(id);

    public void Update(DrinkType drinkType)
        => _context.Update(drinkType);

    public async Task InsertAsync(DrinkType drinkType)
        => await _context.AddAsync(drinkType);

    public void Remove(DrinkType drinkType)
        => _context.Remove(drinkType);
}
