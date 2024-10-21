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

    public Task EditAsync(Drink order)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<Drink>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Drink> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(Drink order)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Drink order)
    {
        throw new NotImplementedException();
    }
}
