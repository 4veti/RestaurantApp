using RestaurantApp.Domain.Contracts.DTOs;

namespace RestaurantApp.Services.Contracts;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllByParamsAsync(OrderQueryParams queryParams);
    Task<bool> GetAllCountByParamsAsync(OrderQueryParams queryParams);
    Task<MenuDto> GetMenuAsync();
    Task<OrderDto?> GetByIdAsync(int orderId);
    Task<string> AddAsync(OrderDto foodDto);
    Task<bool> UpdateAsync(int orderId, OrderDto foodDto);
    Task<bool> DeleteByIdAsync(int orderId);
    Task<bool> MarkPaidAsync(int orderId);
    Task<bool> MarkServedAsync(int orderId);
    Task<(IEnumerable<int>?, string)> GetServedOrderIDsAsync (int oldestNotServedOrderId);
}
