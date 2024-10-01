using RestaurantApp.Core.Contracts;
using RestaurantApp.Infrastructure.Common;

namespace RestaurantApp.Core.Services;

public class OrderService : IOrderService
{
    private readonly IRepository _repository;

    public OrderService(IRepository repository)
    {
        _repository = repository;
    }
}
