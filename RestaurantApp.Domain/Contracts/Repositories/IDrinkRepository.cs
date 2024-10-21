using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Domain.Contracts.Repositories;

public interface IDrinkRepository
{
    Task<IQueryable<Drink>> GetAllAsync();
    Task<Drink> GetByIdAsync(int id);
    Task InsertAsync(Drink order);
    Task EditAsync(Drink order);
    Task RemoveAsync(Drink order);
}
