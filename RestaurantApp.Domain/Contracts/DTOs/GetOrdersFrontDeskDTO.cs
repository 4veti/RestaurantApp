namespace RestaurantApp.Domain.Contracts.DTOs;

public class GetOrdersFrontDeskDTO
{
    public List<OrderDto> NewOrders { get; set; } = new List<OrderDto>();
    public IEnumerable<int>? ServedOrderIDs { get; set; } = new List<int>();
    public string ErrorMessage { get; set; } = string.Empty;
}
