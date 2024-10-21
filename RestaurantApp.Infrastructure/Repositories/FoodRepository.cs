using RestaurantApp.Domain.Contracts.Repositories;
using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Infrastructure.Repositories;

public sealed class FoodRepository : IFoodRepository
{
    public Task EditAsync(Food order)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<Food>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Food> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(Food order)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Food order)
    {
        throw new NotImplementedException();
    }
}
