using RestaurantApp.Contracts;

namespace RestaurantApp.Services.Abstractions;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllByParamsAsync(OrderQueryParams queryParams);
    Task<OrderDto> GetByIdAsync(int id);
    Task AddAsync(OrderDto foodDto);
    Task UpdateAsync(int id, OrderDto foodDto);
    Task DeleteByIdAsync(int id);
}
