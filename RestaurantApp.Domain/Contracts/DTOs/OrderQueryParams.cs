namespace RestaurantApp.Domain.Contracts.DTOs;

public class OrderQueryParams
{
    public int LastOrderId { get; set; }
    public bool? IsPaid { get; set; }
    public bool? OnlyNotServed { get; set; }
    public DateTime? FromDate { get; set; }
    public bool MustHaveFoods { get; set; }
}
