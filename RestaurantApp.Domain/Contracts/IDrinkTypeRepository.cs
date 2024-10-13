using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Domain.Contracts;

public interface IDrinkTypeRepository
{
    Task<IQueryable<DrinkType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DrinkType> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task InsertAsync(DrinkType order);
    Task EditAsync(DrinkType order);
    Task RemoveAsync(DrinkType order);
}
