using RestaurantApp.Contracts;
using RestaurantApp.Services.Abstractions;

namespace RestaurantApp.Services;

internal class OrderService : IOrderService
{
    public Task AddAsync(OrderDto foodDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderDto>> GetAllByParamsAsync(OrderQueryParams queryParams)
    {
        throw new NotImplementedException();
    }

    public Task<OrderDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, OrderDto foodDto)
    {
        throw new NotImplementedException();
    }
}
