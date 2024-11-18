using RestaurantApp.Domain.Contracts.DTOs;

namespace RestaurantApp.Services.Contracts;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllByParamsAsync(OrderQueryParams queryParams);
    Task<OrderDto?> GetByIdAsync(int id);
    Task<bool> AddAsync(OrderDto foodDto);
    Task UpdateAsync(int id, OrderDto foodDto);
    Task<bool> DeleteByIdAsync(int id);
    Task<bool> MarkPaidAsync();
    Task<bool> MarkCompletedAsync();
}
