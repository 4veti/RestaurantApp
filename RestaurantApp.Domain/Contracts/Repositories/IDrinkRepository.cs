using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Domain.Contracts.Repositories;

public interface IDrinkRepository
{
    IQueryable<Drink> GetAllAsync(bool asNoTracking = false);
    Task<Drink?> GetByIdAsync(int id);
    Task InsertAsync(Drink drink);
    void Update(Drink drink);
    void Remove(Drink drink);
}
