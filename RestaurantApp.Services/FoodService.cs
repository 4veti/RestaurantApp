using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Domain.Entities;
using RestaurantApp.Services.Contracts;
using static RestaurantApp.Domain.ErrorMessages;

namespace RestaurantApp.Services;

internal class FoodService : IFoodService
{
    private readonly IRepositoryManager _repositoryManager;

    public FoodService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<string> AddAsync(FoodDto foodDto)
    {
        Food addFood = new Food()
        {
            Name = foodDto.Name,
            NetGrams = foodDto.NetGrams,
            Price = foodDto.Price,
            FoodTypeId = foodDto.FoodTypeId,
            Created = DateTime.Now,
            Modified = DateTime.Now
        };

        List<string> validationResult = await ValidateFoodDto(foodDto);

        if (validationResult.Any())
        {
            return string.Join(" ", validationResult);
        }

        await _repositoryManager.FoodRepository.InsertAsync(addFood);
        await _repositoryManager.UnitOfWork.SaveChangesAsync();

        foodDto.Id = addFood.Id;

        return string.Empty;
    }

    public async Task<bool?> DeleteByIdAsync(int id)
    {
        try
        {
            Food? deleteFood = await _repositoryManager.FoodRepository.GetByIdAsync(id);

            if (deleteFood is null)
            {
                return null;
            }

            _repositoryManager.FoodRepository.Remove(deleteFood);
            bool isDeleted = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

            return isDeleted;
        }
        catch (Exception)
        {
            return false;
        }
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
                FoodTypeName = f.FoodType.Name,
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
                FoodTypeName = food.FoodType.Name,
            };
        }

        return foodDto;
    }

    public async Task<bool?> UpdateAsync(int id, FoodDto foodDto)
    {
        try
        {
            Food? originalFood = await _repositoryManager.FoodRepository.GetByIdAsync(id);

            if (originalFood is null)
            {
                return null;
            }

            originalFood.Name = foodDto.Name;
            originalFood.Modified = DateTime.Now;

            _repositoryManager.FoodRepository.Update(originalFood);
            bool successfulUpdate = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

            return successfulUpdate;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private async Task<List<string>> ValidateFoodDto(FoodDto dto)
    {
        List<string> result = new List<string>();

        bool nameExists = await _repositoryManager.FoodRepository
            .GetAllAsync()
            .Where(f => f.Name == dto.Name)
            .AnyAsync();

        if (nameExists)
        {
            result.Add(string.Format(FoodNameAlreadyExists, dto.Name));
        }

        bool isValidFoodTypeId = await _repositoryManager.FoodTypeRepository
            .GetAllAsync()
            .Where(ft => ft.Id == dto.FoodTypeId)
            .AnyAsync();

        if (dto.FoodTypeId < 1 || isValidFoodTypeId == false)
        {
            result.Add(string.Format(InvalidFoodTypeId, dto.FoodTypeId));
        }

        return result;
    }
}
