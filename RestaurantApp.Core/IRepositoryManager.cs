using RestaurantApp.Domain.Contracts.Repositories;

namespace RestaurantApp.Services.Abstractions;

public interface IRepositoryManager
{
    IDrinkRepository DrinkRepository { get; }
    IDrinkTypeRepository DrinkTypeRepository { get; }
    IFoodRepository FoodRepository { get; }
    IFoodTypeRepository FoodTypeRepository { get; }
    IOrderRepository OrderRepository { get; }
    IFoodOrderRepository FoodOrderRepository { get; }
    IUnitOfWork UnitOfWork { get; }
}
