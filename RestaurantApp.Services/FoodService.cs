using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Services.Abstractions;

namespace RestaurantApp.Services;

internal class FoodService : IFoodService
{
    public Task AddAsync(FoodDto foodDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FoodDto>> GetAllByFoodTypeIdAsync(int foodTypeId)
    {
        throw new NotImplementedException();
    }

    public Task<FoodDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, FoodDto foodDto)
    {
        throw new NotImplementedException();
    }
}
