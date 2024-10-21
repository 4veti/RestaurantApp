using RestaurantApp.Domain.Contracts.Repositories;

namespace RestaurantApp.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
