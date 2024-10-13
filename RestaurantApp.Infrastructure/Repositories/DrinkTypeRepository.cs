using RestaurantApp.Domain.Contracts;
using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Infrastructure.Repositories
{
    public sealed class DrinkTypeRepository : IDrinkTypeRepository
    {
        public Task EditAsync(DrinkType order)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<DrinkType>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<DrinkType> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(DrinkType order)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(DrinkType order)
        {
            throw new NotImplementedException();
        }
    }
}
