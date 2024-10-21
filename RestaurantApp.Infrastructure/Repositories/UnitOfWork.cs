using RestaurantApp.Domain.Contracts.Repositories;

namespace RestaurantApp.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly RestaurantAppDbContext _context;

    public UnitOfWork(RestaurantAppDbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
