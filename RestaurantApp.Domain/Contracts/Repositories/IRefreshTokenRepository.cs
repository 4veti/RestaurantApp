using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Domain.Contracts.Repositories;

public interface IRefreshTokenRepository
{
    IQueryable<RefreshToken> GetAll(bool asNoTracking = false);
    void Insert(RefreshToken refreshToken);
    void Remove(RefreshToken refreshToken);
}
