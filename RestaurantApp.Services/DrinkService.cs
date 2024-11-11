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
            Created = dto.Created,
            Modified = dto.Modified,
            AlcoholPercentage = dto.AlcoholPercentage,
        };

        await _repositoryManager.DrinkRepository.InsertAsync(drink);
        await _repositoryManager.UnitOfWork.SaveChangesAsync();
    }

    public Task DeleteByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DrinkDto>> GetAllByDrinkTypeIdAsync(int drinkType)
    {
        throw new NotImplementedException();
    }

    public Task<DrinkDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, DrinkDto dto)
    {
        throw new NotImplementedException();
    }
}
