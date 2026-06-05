using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain.Contracts.Repositories;
using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Infrastructure.Repositories;

public class TerminalRepository : ITerminalRepository
{
    private readonly RestaurantAppDbContext _context;

    public TerminalRepository(RestaurantAppDbContext context)
    {
        _context = context;
    }

    public async Task<Terminal?> GetBySecretAsync(string hashedSecret, bool asNoTracking = false)
    {
        IQueryable<Terminal> terminals = _context.Set<Terminal>()
            .Include(t => t.TerminalType);

        if (asNoTracking)
        {
            terminals = terminals.AsNoTracking();
        }

        return await terminals.Where(t => t.HashedSecret == hashedSecret).FirstOrDefaultAsync();
    }
}
