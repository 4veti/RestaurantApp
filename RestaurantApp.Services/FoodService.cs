using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Domain.Entities;
using RestaurantApp.Services.Contracts;
using RestaurantApp.Domain;
using static RestaurantApp.Domain.Constants;

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
        List<string> validationResult = await ValidateFoodDto(foodDto);

        if (validationResult.Any())
        {
            return string.Join(" ", validationResult);
        }

        Food addFood = new Food()
        {
            Name = foodDto.Name,
            NetGrams = foodDto.NetGrams,
            Price = foodDto.Price,
            FoodTypeId = foodDto.FoodTypeId,
            Created = DateTime.Now,
            Modified = DateTime.Now
        };

        await _repositoryManager.FoodRepository.InsertAsync(addFood);
        bool successfulInsert = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

        if (successfulInsert == false)
        {
            return string.Format(ErrorMessages.FailedToInsert, typeof(Food));
        }

        return string.Empty;
    }

    public async Task<string?> DeleteByIdAsync(int id)
    {
        if (id < 1)
        {
            return ErrorMessages.IdMustBeAboveZero;
        }

        Food? deleteFood = await _repositoryManager.FoodRepository.GetByIdAsync(id);

        if (deleteFood is null)
        {
            return null;
        }

        _repositoryManager.FoodRepository.Remove(deleteFood);
        bool isDeleted = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

        if (isDeleted == false)
        {
            return string.Format(ErrorMessages.FailedToDelete, typeof(Food));
        }

        return string.Empty;
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

    public async Task<string?> UpdateAsync(FoodDto foodDto)
    {
        if (foodDto.Id < 1)
        {
            return ErrorMessages.IdMustBeAboveZero;
        }

        Food? originalFood = await _repositoryManager.FoodRepository.GetByIdAsync(foodDto.Id);

        if (originalFood is null)
        {
            return null;
        }

        List<string> validationResult = await ValidateFoodDto(foodDto);

        if (validationResult.Any())
        {
            return string.Join(" ", validationResult);
        }

        originalFood.Name = foodDto.Name;
        originalFood.NetGrams = foodDto.NetGrams;
        originalFood.Price = foodDto.Price;
        originalFood.FoodTypeId = foodDto.FoodTypeId;
        originalFood.Modified = DateTime.Now;

        _repositoryManager.FoodRepository.Update(originalFood);
        bool successfulUpdate = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

        if (successfulUpdate == false)
        {
            return string.Format(ErrorMessages.FailedToUpdate, typeof(Food));
        }

        return string.Empty;
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
            result.Add(string.Format(ErrorMessages. FoodNameAlreadyExists, dto.Name));
        }

        bool isValidFoodTypeId = dto.FoodTypeId > 0 && await _repositoryManager.FoodTypeRepository
            .GetAllAsync()
            .Where(ft => ft.Id == dto.FoodTypeId)
            .AnyAsync();

        if (isValidFoodTypeId == false)
        {
            result.Add(string.Format(ErrorMessages.InvalidFoodTypeId, dto.FoodTypeId));
        }

        if (dto.NetGrams < NetGramsMin || dto.NetGrams > NetGramsMax)
        {
            result.Add(string.Format(ErrorMessages.NetGramsOutOfRange, NetGramsMin, NetGramsMax));
        }

        if ((double)dto.Price < MinPrice || (double)dto.Price > MaxPrice)
        {
            result.Add(string.Format(ErrorMessages.PriceOutOfRange, MinPrice, MaxPrice));
        }

        return result;
    }
}
