
namespace RestaurantApp.Infrastructure.Common;

public class Repository : IRepository
{
    public Task AddAsync<T>(T entity) where T : class
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> All<T>() where T : class
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> AllAsReadOnly<T>() where T : class
    {
        throw new NotImplementedException();
    }

    public Task<T?> GetByIdAsync<T>(object id) where T : class
    {
        throw new NotImplementedException();
    }

    public void Remove<T>(T entity) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
