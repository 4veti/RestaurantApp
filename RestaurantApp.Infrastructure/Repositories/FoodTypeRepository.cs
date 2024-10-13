using RestaurantApp.Domain.Contracts;
using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Infrastructure.Repositories;

public sealed class FoodTypeRepository : IFoodTypeRepository
{
    public Task EditAsync(FoodType order)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<FoodType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FoodType> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(FoodType order)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(FoodType order)
    {
        throw new NotImplementedException();
    }
}
