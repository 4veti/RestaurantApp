using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Domain.Contracts.Repositories;

public interface IDrinkTypeRepository
{
    Task<IQueryable<DrinkType>> GetAllAsync();
    Task<DrinkType> GetByIdAsync(int id);
    Task InsertAsync(DrinkType order);
    Task EditAsync(DrinkType order);
    Task RemoveAsync(DrinkType order);
}
