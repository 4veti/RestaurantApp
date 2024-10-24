using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Domain.Contracts.Repositories;

public interface IDrinkTypeRepository
{
    IQueryable<DrinkType> GetAllAsync(bool asNoTracking = false);
    Task<DrinkType?> GetByIdAsync(int id);
    Task InsertAsync(DrinkType drinkType);
    void Update(DrinkType drinkType);
    void Remove(DrinkType drinkType);
}
