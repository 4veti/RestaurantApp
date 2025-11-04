using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Domain.Entities;
using RestaurantApp.Services.Contracts;
using RestaurantApp.Domain;
using static RestaurantApp.Domain.Constants;

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
        if (dto.Name.Length < FoodNameMinLength || dto.Name.Length > FoodNameMaxLength)
        {
            return string.Format(ErrorMessages.InvalidNameLength, FoodNameMinLength, FoodNameMaxLength);
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
            return string.Format(ErrorMessages.FailedToInsert, typeof(FoodType));
        }

        return string.Empty;
    }

    public async Task<string?> DeleteByIdAsync(int id)
    {
        if (id < 1)
        {
            return ErrorMessages.IdMustBeAboveZero;
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
            return string.Format(ErrorMessages.FailedToDelete, typeof(FoodType));
        }

        return string.Empty;
    }

    public async Task<IEnumerable<FoodTypeDto>> GetAllAsync()
    {
        List<FoodTypeDto> foodTypes = await _repositoryManager.FoodTypeRepository
            .GetAllAsync(true)
            .Select(d => new FoodTypeDto()
            {
                Id = d.Id,
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
            return ErrorMessages.IdMustBeAboveZero;
        }


        if (dto.Name.Length < FoodNameMinLength || dto.Name.Length > FoodNameMaxLength)
        {
            return string.Format(ErrorMessages.InvalidNameLength, FoodNameMinLength, FoodNameMaxLength);
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
            return string.Format(ErrorMessages.FailedToUpdate, typeof(FoodType));
        }

        return string.Empty;
    }
}
