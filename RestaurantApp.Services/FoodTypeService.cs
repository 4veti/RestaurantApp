using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Domain.Entities;
using RestaurantApp.Services.Contracts;

namespace RestaurantApp.Services;

internal class FoodTypeService : IFoodTypeService
{
    private readonly IRepositoryManager _repositoryManager;

    public FoodTypeService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task AddAsync(FoodTypeDto dto)
    {
        FoodType addFoodType = new FoodType()
        {
            Name = dto.Name,
            Created = DateTime.Now,
            Modified = DateTime.Now,
        };

        await _repositoryManager.FoodTypeRepository.InsertAsync(addFoodType);
        await _repositoryManager.UnitOfWork.SaveChangesAsync();
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        FoodType? deleteFoodType = await _repositoryManager.FoodTypeRepository.GetByIdAsync(id);

        if (deleteFoodType is not null)
        {
            _repositoryManager.FoodTypeRepository.Remove(deleteFoodType);
            bool isDeleted = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

            return isDeleted;
        }

        return false;
    }

    public async Task<IEnumerable<FoodTypeDto>> GetAllAsync()
    {
        List<FoodTypeDto> foodTypes = await _repositoryManager.DrinkTypeRepository
            .GetAllAsync(true)
            .Select(d => new FoodTypeDto()
            {
                Name = d.Name
            })
            .ToListAsync();

        return foodTypes;
    }

    public async Task<FoodTypeDto?> GetByIdAsync(int id)
    {
        FoodType? foodType = await _repositoryManager.FoodTypeRepository.GetByIdAsync(id);
        FoodTypeDto? foodTypeDto = null;

        if (foodType is not null)
        {
            foodTypeDto = new FoodTypeDto()
            {
                Name = foodType.Name
            };
        }

        return foodTypeDto;
    }

    public async Task<bool> UpdateAsync(int id, FoodTypeDto dto)
    {
        FoodType? originalFoodType = await _repositoryManager.FoodTypeRepository.GetByIdAsync(id);

        if (originalFoodType is null)
        {
            return false;
        }

        originalFoodType.Name = dto.Name;
        originalFoodType.Modified = DateTime.Now;

        _repositoryManager.FoodTypeRepository.Update(originalFoodType);
        bool successfulUpdate = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

        return successfulUpdate;
    }
}
