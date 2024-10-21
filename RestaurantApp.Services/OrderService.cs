using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Services.Contracts;

namespace RestaurantApp.Services;

internal class OrderService : IOrderService
{
    private readonly IRepositoryManager _repositoryManager;

    public OrderService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

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
