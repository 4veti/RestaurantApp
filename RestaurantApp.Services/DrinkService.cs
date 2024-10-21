using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Services.Contracts;

namespace RestaurantApp.Services;

internal class DrinkService : IDrinkService
{
    private readonly IRepositoryManager _repositoryManager;

    public DrinkService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

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
