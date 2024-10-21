using RestaurantApp.Domain.Contracts.Repositories;
using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Infrastructure.Repositories;

public sealed class DrinkRepository : IDrinkRepository
{
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
