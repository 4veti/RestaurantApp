using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Services.Contracts;

namespace RestaurantApp.Services;

internal class FoodTypeService : IFoodTypeService
{
    private readonly IRepositoryManager _repositoryManager;

    public FoodTypeService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

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
