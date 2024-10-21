using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Services.Abstractions;

namespace RestaurantApp.Services;

internal class DrinkService : IDrinkService
{
    public Task AddAsync(DrinkDto dto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DrinkDto>> GetAllByDrinkTypeIdAsync(int drinkType)
    {
        throw new NotImplementedException();
    }

    public Task<DrinkDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, DrinkDto dto)
    {
        throw new NotImplementedException();
    }
}
