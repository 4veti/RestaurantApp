using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Domain.Entities;
using RestaurantApp.Services.Contracts;

namespace RestaurantApp.Services;

internal class FoodService : IFoodService
{
    private readonly IRepositoryManager _repositoryManager;

    public FoodService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task AddAsync(FoodDto foodDto)
    {
        Food AddFood = new Food()
        {
            Name = foodDto.Name,
            NetGrams = foodDto.NetGrams,
            Price = foodDto.Price,
            FoodTypeId = foodDto.FoodTypeId,
            Created = DateTime.Now,
            Modified = DateTime.Now
        };

        await _repositoryManager.FoodRepository.InsertAsync(AddFood);
        await _repositoryManager.UnitOfWork.SaveChangesAsync();
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        Food? deleteFood = await _repositoryManager.FoodRepository.GetByIdAsync(id);

        if (deleteFood is not null)
        {
            _repositoryManager.FoodRepository.Remove(deleteFood);
            bool isDeleted = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

            return isDeleted;
        }

        return false;
    }

    public async Task<IEnumerable<FoodDto>> GetAllByFoodTypeIdAsync(int foodTypeId)
    {
        List<FoodDto> foods = await _repositoryManager.FoodRepository
            .GetAllAsync(true)
            .Include(f => f.FoodType)
            .Select(f => new FoodDto()
            {
                Name = f.Name,
                NetGrams = f.NetGrams,
                Price = f.Price,
                FoodTypeId = foodTypeId,
                FoodType = f.FoodType.Name,
            })
            .ToListAsync();

        return foods;
    }

    public async Task<FoodDto?> GetByIdAsync(int id)
    {
        Food? food = await _repositoryManager.FoodRepository.GetByIdAsync(id);
        FoodDto? foodDto = null;

        if (food is not null)
        {
            foodDto = new FoodDto()
            {
                Name = food.Name,
                NetGrams = food.NetGrams,
                Price = food.Price,
                FoodTypeId = food.FoodType.Id,
                FoodType = food.FoodType.Name,
            };
        }

        return foodDto;
    }

    public async Task<bool> UpdateAsync(int id, FoodDto foodDto)
    {
        Food? originalFood = await _repositoryManager.FoodRepository.GetByIdAsync(id);

        if (originalFood is null)
        {
            return false;
        }

        originalFood.Name = foodDto.Name;
        originalFood.Modified = DateTime.Now;

        _repositoryManager.FoodRepository.Update(originalFood);
        bool successfulUpdate = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

        return successfulUpdate;
    }
}
