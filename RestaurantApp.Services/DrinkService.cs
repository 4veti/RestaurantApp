using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Domain.Entities;
using RestaurantApp.Services.Contracts;

namespace RestaurantApp.Services;

internal class DrinkService : IDrinkService
{
    private readonly IRepositoryManager _repositoryManager;

    public DrinkService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task AddAsync(DrinkDto dto)
    {
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
        await _repositoryManager.UnitOfWork.SaveChangesAsync();
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        Drink? drinkToDelete = await _repositoryManager.DrinkRepository.GetByIdAsync(id);

        if (drinkToDelete is not null)
        {
            _repositoryManager.DrinkRepository.Remove(drinkToDelete);
            bool successfulDelete = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

            return successfulDelete;
        }

        return false;
    }

    public async Task<IEnumerable<DrinkDto>> GetAllByDrinkTypeIdAsync(int drinkType)
    {
        List<DrinkDto> drinks = await _repositoryManager.DrinkRepository
            .GetAllAsync(true)
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

    public async Task<bool> UpdateAsync(int id, DrinkDto dto)
    {
        Drink? originalDrink = await _repositoryManager.DrinkRepository.GetByIdAsync(id);

        if (originalDrink is null)
        {
            return false;
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

        return successfulUpdate;
    }
}
