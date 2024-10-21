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

    public Task EditAsync(DrinkType order)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<DrinkType>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<DrinkType> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(DrinkType order)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(DrinkType order)
    {
        throw new NotImplementedException();
    }
}
