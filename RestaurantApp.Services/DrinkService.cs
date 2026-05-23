using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Domain.Entities;
using RestaurantApp.Services.Contracts;
using RestaurantApp.Domain;
using static RestaurantApp.Domain.Constants;

namespace RestaurantApp.Services;

internal class DrinkService : IDrinkService
{
    private readonly IRepositoryManager _repositoryManager;

    public DrinkService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<string> AddAsync(DrinkDto dto)
    {
        List<string> validationResult = await ValidateDrinkDto(dto);

        if (validationResult.Any())
        {
            return string.Join(" ", validationResult);
        }

        Drink drink = new Drink()
        {
            Name = dto.Name,
            Price = dto.Price,
            DrinkTypeId = dto.DrinkTypeId,
            Millilitres = dto.Millilitres,
            IsAlcoholic = dto.IsAlcoholic,
            Created = DateTime.Now,
            Modified = DateTime.Now,
            AlcoholPercentage = dto.AlcoholPercentage,
        };

        await _repositoryManager.DrinkRepository.InsertAsync(drink);
        bool successfulInsert = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

        if (successfulInsert == false)
        {
            return string.Format(ErrorMessages.FailedToInsert, typeof(Drink));
        }

        return string.Empty;
    }

    public async Task<string?> DeleteByIdAsync(int id)
    {
        if (id < 1)
        {
            return ErrorMessages.IdMustBeAboveZero;
        }

        Drink? drinkToDelete = await _repositoryManager.DrinkRepository.GetByIdAsync(id);

        if (drinkToDelete is null)
        {
            return null;
        }

        _repositoryManager.DrinkRepository.Remove(drinkToDelete);
        bool isDeleted = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

        if (isDeleted == false)
        {
            return string.Format(ErrorMessages.FailedToDelete, typeof(Drink));
        }

        return string.Empty;
    }

    public async Task<IEnumerable<DrinkDto>> GetAllByDrinkTypeIdAsync(int drinkType)
    {
        List<DrinkDto> drinks = await _repositoryManager.DrinkRepository
            .GetAll(true)
            .Include(d => d.DrinkType)
            .Where(d => d.DrinkTypeId == drinkType)
            .Select(d => new DrinkDto()
            {
                Name = d.Name,
                Price = d.Price,
                DrinkTypeId = drinkType,
                DrinkType = d.DrinkType.Name,
                Millilitres = d.Millilitres,
                IsAlcoholic = d.IsAlcoholic,
                AlcoholPercentage = d.AlcoholPercentage,
            })
            .ToListAsync();

        return drinks;
    }

    public async Task<DrinkDto?> GetByIdAsync(int id)
    {
        Drink? drink = await _repositoryManager.DrinkRepository.GetByIdAsync(id);
        DrinkDto? drinkDto = null;

        if (drink is not null)
        {
            drinkDto = new DrinkDto()
            {
                Name = drink.Name,
                Price = drink.Price,
                DrinkTypeId = drink.DrinkTypeId,
                DrinkType = drink.DrinkType.Name,
                Millilitres = drink.Millilitres,
                IsAlcoholic = drink.IsAlcoholic,
                AlcoholPercentage = drink.AlcoholPercentage,
            };
        }

        return drinkDto;
    }

    public async Task<string?> UpdateAsync(DrinkDto dto)
    {
        if (dto.Id < 0)
        {
            return ErrorMessages.IdMustBeAboveZero;
        }

        Drink? originalDrink = await _repositoryManager.DrinkRepository.GetByIdAsync(dto.Id);

        if (originalDrink is null)
        {
            return null;
        }

        bool isNameChanged = dto.Name != originalDrink.Name;
        List<string> validationResult = await ValidateDrinkDto(dto, isNameChanged);

        if (validationResult.Any())
        {
            return string.Join(" ", validationResult);
        }

        originalDrink.Name = dto.Name;
        originalDrink.Price = dto.Price;
        originalDrink.DrinkTypeId = dto.DrinkTypeId;
        originalDrink.Millilitres = dto.Millilitres;
        originalDrink.IsAlcoholic = dto.IsAlcoholic;
        originalDrink.AlcoholPercentage = dto.AlcoholPercentage;
        originalDrink.Modified = DateTime.Now;

        _repositoryManager.DrinkRepository.Update(originalDrink);
        bool successfulUpdate = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

        if (successfulUpdate == false)
        {
            return string.Format(ErrorMessages.FailedToInsert, typeof(Drink));
        }

        return string.Empty;
    }

    private async Task<List<string>> ValidateDrinkDto(DrinkDto dto, bool isNameChanged = true)
    {
        List<string> result = new List<string>();

        if (isNameChanged)
        {
            bool nameExists = await _repositoryManager.DrinkRepository
                .GetAll()
                .Where(d => d.Name == dto.Name)
                .AnyAsync();

            if (nameExists)
            {
                result.Add(string.Format(ErrorMessages.FoodNameAlreadyExists, dto.Name));
            }
        }

        bool isValidDrinkTypeId = dto.DrinkTypeId > 0 && await _repositoryManager.DrinkTypeRepository
            .GetAllAsync()
            .Where(dt => dt.Id == dto.DrinkTypeId)
            .AnyAsync();

        if (isValidDrinkTypeId == false)
        {
            result.Add(string.Format(ErrorMessages.InvalidDrinkTypeId, dto.DrinkTypeId));
        }

        if (dto.Millilitres < MinMillilitres || dto.Millilitres > MaxMillilitres)
        {
            result.Add(string.Format(ErrorMessages.MillilitresOutOfRange, MinMillilitres, MaxMillilitres));
        }

        if ((double)dto.Price < MinPrice || (double)dto.Price > MaxPrice)
        {
            result.Add(string.Format(ErrorMessages.PriceOutOfRange, MinPrice, MaxPrice));
        }

        if (dto.IsAlcoholic)
        {
            if (dto.AlcoholPercentage is null)
            {
                result.Add(ErrorMessages.AlcoholByVolumeIsMandatory);
            }
            else if (dto.AlcoholPercentage < MinAlcoholPercentage || dto.AlcoholPercentage > MaxAlcoholPercentage)
            {
                result.Add(string.Format(ErrorMessages.AlcoholPercentageOutOfRange, MinAlcoholPercentage, MaxAlcoholPercentage));
            }
        }
        else
        {
            if (dto.AlcoholPercentage is not null)
            {
                result.Add(ErrorMessages.AlcoholByVolumeIsOnlyForAlcoholicDrinks);
            }
        }

        return result;
    }
}
