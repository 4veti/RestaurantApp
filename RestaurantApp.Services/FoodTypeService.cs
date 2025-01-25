using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Domain.Entities;
using RestaurantApp.Services.Contracts;
using static RestaurantApp.Domain.Constants;
using static RestaurantApp.Domain.ErrorMessages;

namespace RestaurantApp.Services;

internal class FoodTypeService : IFoodTypeService
{
    private readonly IRepositoryManager _repositoryManager;

    public FoodTypeService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<string> AddAsync(FoodTypeDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            return NameCannotBeNullOrEmpty;
        }

        if (dto.Name.Length < FoodNameMinLength || dto.Name.Length > FoodNameMaxLength)
        {
            return string.Format(InvalidNameLength, FoodNameMinLength, FoodNameMaxLength);
        }

        FoodType addFoodType = new FoodType()
        {
            Name = dto.Name,
            Created = DateTime.Now,
            Modified = DateTime.Now,
        };

        await _repositoryManager.FoodTypeRepository.InsertAsync(addFoodType);
        bool successfulInsert = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

        if (successfulInsert == false)
        {
            return string.Format(FailedToInsert, typeof(FoodType));
        }

        return string.Empty;
    }

    public async Task<string?> DeleteByIdAsync(int id)
    {
        if (id < 1)
        {
            return IdMustBeAboveZero;
        }

        FoodType? deleteFoodType = await _repositoryManager.FoodTypeRepository.GetByIdAsync(id);

        if (deleteFoodType is null)
        {
            return null;
        }

        _repositoryManager.FoodTypeRepository.Remove(deleteFoodType);
        bool isDeleted = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

        if (isDeleted == false)
        {
            return string.Format(FailedToDelete, typeof(FoodType));
        }

        return string.Empty;
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

    public async Task<string?> UpdateAsync(FoodTypeDto dto)
    {
        if (dto.Id < 1)
        {
            return IdMustBeAboveZero;
        }


        if (dto.Name.Length < FoodNameMinLength || dto.Name.Length > FoodNameMaxLength)
        {
            return string.Format(InvalidNameLength, FoodNameMinLength, FoodNameMaxLength);
        }

        FoodType? originalFoodType = await _repositoryManager.FoodTypeRepository.GetByIdAsync(dto.Id);

        if (originalFoodType is null)
        {
            return null;
        }

        originalFoodType.Name = dto.Name;
        originalFoodType.Modified = DateTime.Now;

        _repositoryManager.FoodTypeRepository.Update(originalFoodType);
        bool successfulUpdate = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

        if (successfulUpdate == false)
        {
            return string.Format(FailedToUpdate, typeof(FoodType));
        }

        return string.Empty;
    }
}
