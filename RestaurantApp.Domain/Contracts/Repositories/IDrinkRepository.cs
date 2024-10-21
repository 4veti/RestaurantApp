using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Domain.Contracts.Repositories;

public interface IDrinkRepository
{
    Task<IQueryable<Drink>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Drink> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task InsertAsync(Drink order);
    Task EditAsync(Drink order);
    Task RemoveAsync(Drink order);
}
