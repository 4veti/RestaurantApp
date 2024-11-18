using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Domain.Entities;
using RestaurantApp.Services.Contracts;
using System.IO;

namespace RestaurantApp.Services;

internal class DrinkTypeService : IDrinkTypeService
{
    private readonly IRepositoryManager _repositoryManager;

    public DrinkTypeService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task AddAsync(DrinkTypeDto dto)
    {
        DrinkType addDrinkType = new DrinkType()
        {
            Name = dto.Name,
            Created = DateTime.Now,
            Modified = DateTime.Now,
        };

        await _repositoryManager.DrinkTypeRepository.InsertAsync(addDrinkType);
        await _repositoryManager.UnitOfWork.SaveChangesAsync();
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        DrinkType? deleteDrinkType = await _repositoryManager.DrinkTypeRepository.GetByIdAsync(id);

        if (deleteDrinkType is not null)
        {
            _repositoryManager.DrinkTypeRepository.Remove(deleteDrinkType);
            bool isDeleted = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

            return isDeleted;
        }

        return false;
    }

    public async Task<IEnumerable<DrinkTypeDto>> GetAllAsync()
    {
        List<DrinkTypeDto> drinkTypes = await _repositoryManager.DrinkTypeRepository
            .GetAllAsync(true)
            .Select(d => new DrinkTypeDto()
            {
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

    public async Task<bool> UpdateAsync(int id, DrinkTypeDto dto)
    {
        DrinkType? originalDrinkType = await _repositoryManager.DrinkTypeRepository.GetByIdAsync(id);

        if (originalDrinkType is null)
        {
            return false;
        }

        originalDrinkType.Name = dto.Name;
        originalDrinkType.Modified = DateTime.Now;

        _repositoryManager.DrinkTypeRepository.Update(originalDrinkType);
        bool successfulUpdate = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

        return successfulUpdate;
    }
}
