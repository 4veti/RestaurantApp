using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Services.Abstractions;

namespace RestaurantApp.Services;

internal class FoodTypeService : IFoodTypeService
{
    public Task AddAsync(FoodTypeDto dto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FoodTypeDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<FoodTypeDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, FoodTypeDto dto)
    {
        throw new NotImplementedException();
    }
}
