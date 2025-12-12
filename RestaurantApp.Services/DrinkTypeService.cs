using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Domain.Entities;
using RestaurantApp.Services.Contracts;
using static RestaurantApp.Domain.Constants;

namespace RestaurantApp.Services;

internal class DrinkTypeService : IDrinkTypeService
{
    private readonly IRepositoryManager _repositoryManager;

    public DrinkTypeService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<string> AddAsync(DrinkTypeDto dto)
    {
        if (dto.Name.Length < MinDrinkTypeNameLength || dto.Name.Length > MaxDrinkTypeNameLength)
        {
            return string.Format(ErrorMessages.InvalidNameLength, MinDrinkTypeNameLength, MaxDrinkTypeNameLength);
        }

        DrinkType addDrinkType = new DrinkType()
        {
            Name = dto.Name,
            Created = DateTime.Now,
            Modified = DateTime.Now,
        };

        await _repositoryManager.DrinkTypeRepository.InsertAsync(addDrinkType);
        bool successfulInsert = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

        if (successfulInsert == false)
        {
            return string.Format(ErrorMessages.FailedToInsert, typeof(DrinkType));
        }

        return string.Empty;
    }

    public async Task<string?> DeleteByIdAsync(int id)
    {
        if (id < 1)
        {
            return ErrorMessages.IdMustBeAboveZero;
        }

        DrinkType? deleteDrinkType = await _repositoryManager.DrinkTypeRepository.GetByIdAsync(id);

        if (deleteDrinkType is null)
        {
            return null;
        }

        _repositoryManager.DrinkTypeRepository.Remove(deleteDrinkType);
        bool isDeleted = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

        if (isDeleted == false)
        {
            return string.Format(ErrorMessages.FailedToDelete, typeof(DrinkType));
        }

        return string.Empty;
    }

    public async Task<IEnumerable<DrinkTypeDto>> GetAllAsync()
    {
        List<DrinkTypeDto> drinkTypes = await _repositoryManager.DrinkTypeRepository
            .GetAllAsync(true)
            .Select(d => new DrinkTypeDto()
            {
                Id = d.Id,
                Name = d.Name
            })
            .ToListAsync();

        return drinkTypes;
    }

    public async Task<DrinkTypeDto?> GetByIdAsync(int id)
    {
        DrinkType? drinkType = await _repositoryManager.DrinkTypeRepository.GetByIdAsync(id);
        DrinkTypeDto? drinkTypeDto = null;

        if (drinkType is not null)
        {
            drinkTypeDto = new DrinkTypeDto()
            {
                Name = drinkType.Name
            };
        }

        return drinkTypeDto;
    }

    public async Task<string?> UpdateAsync(DrinkTypeDto dto)
    {
        if (dto.Name.Length < MinDrinkTypeNameLength || dto.Name.Length > MaxDrinkTypeNameLength)
        {
            return string.Format(ErrorMessages.InvalidNameLength, MinDrinkTypeNameLength, MaxDrinkTypeNameLength);
        }

        DrinkType? originalDrinkType = await _repositoryManager.DrinkTypeRepository.GetByIdAsync(dto.Id);

        if (originalDrinkType is null)
        {
            return null;
        }

        originalDrinkType.Name = dto.Name;
        originalDrinkType.Modified = DateTime.Now;

        _repositoryManager.DrinkTypeRepository.Update(originalDrinkType);
        bool successfulUpdate = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

        if (successfulUpdate == false)
        {
            return string.Format(ErrorMessages.FailedToUpdate, typeof(DrinkType));
        }

        return string.Empty;
    }
}
