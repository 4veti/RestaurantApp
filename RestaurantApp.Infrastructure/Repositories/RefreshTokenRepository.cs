using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain.Contracts.Repositories;
using RestaurantApp.Domain.Entities;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly RestaurantAppDbContext _context;

    public RefreshTokenRepository(RestaurantAppDbContext context)
    {
        _context = context;
    }

    public IQueryable<RefreshToken> GetAll(bool asNoTracking = false)
    {
        IQueryable<RefreshToken> refreshTokens = _context.Set<RefreshToken>();

        if (asNoTracking)
        {
            refreshTokens = refreshTokens.AsNoTracking();
        }

        return refreshTokens;
    }

    public void Insert(RefreshToken refreshToken)
        => _context.Add(refreshToken);

    public void Remove(RefreshToken refreshToken)
        => _context.Remove(refreshToken);
}
