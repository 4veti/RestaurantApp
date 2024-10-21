namespace RestaurantApp.Domain.Contracts.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
