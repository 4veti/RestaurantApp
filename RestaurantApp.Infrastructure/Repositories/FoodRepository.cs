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

    public Task EditAsync(Food order)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<Food>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Food> GetByIdAsync(int id)
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
