using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Services.Contracts;

namespace RestaurantApp.Services;

internal class FoodService : IFoodService
{
    private readonly IRepositoryManager _repositoryManager;

    public FoodService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

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
