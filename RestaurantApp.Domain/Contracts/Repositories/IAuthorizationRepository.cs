using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Domain.Contracts.Repositories;

public interface ITerminalRepository
{
    Task<Terminal?> GetBySecretAsync(string hashedSecret, bool asNoTracking = false);
}
