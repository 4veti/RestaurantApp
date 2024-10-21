using RestaurantApp.Domain.Contracts.Repositories;

namespace RestaurantApp.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
