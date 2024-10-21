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

    public Task EditAsync(FoodType order)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<FoodType>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<FoodType> GetByIdAsync(int id)
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
