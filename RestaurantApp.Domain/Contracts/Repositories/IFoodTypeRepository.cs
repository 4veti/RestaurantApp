using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Domain.Contracts.Repositories;

public interface IFoodTypeRepository
{
    Task<IQueryable<FoodType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<FoodType> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task InsertAsync(FoodType order);
    Task EditAsync(FoodType order);
    Task RemoveAsync(FoodType order);
}
