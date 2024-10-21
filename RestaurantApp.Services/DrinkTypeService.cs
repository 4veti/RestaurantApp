using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Services.Abstractions;

namespace RestaurantApp.Services;

internal class DrinkTypeService : IDrinkTypeService
{
    public Task AddAsync(DrinkTypeDto dto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DrinkTypeDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<DrinkTypeDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, DrinkTypeDto dto)
    {
        throw new NotImplementedException();
    }
}
