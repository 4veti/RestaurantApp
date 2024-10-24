using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain.Contracts.Repositories;
using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Infrastructure.Repositories;

public sealed class DrinkRepository : IDrinkRepository
{
    private readonly RestaurantAppDbContext _context;

    public DrinkRepository(RestaurantAppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Drink> GetAllAsync(bool asNoTracking = false)
    {
        IQueryable<Drink> drinks = _context.Set<Drink>();

        if (asNoTracking)
        {
            drinks = drinks.AsNoTracking();
        }

        return drinks;
    }

    public async Task<Drink?> GetByIdAsync(int id)
        => await _context.FindAsync<Drink>(id);

    public void Update(Drink drink)
        => _context.Update(drink);

    public void Remove(Drink drink)
        => _context.Remove(drink);

    public async Task InsertAsync(Drink drink)
        => await _context.AddAsync(drink);
}
