using RestaurantApp.Domain.Contracts.Repositories;
using RestaurantApp.Infrastructure.Repositories;

namespace RestaurantApp.Services.Contracts;

public interface IRepositoryManager
{
    IDrinkRepository DrinkRepository { get; }
    IDrinkTypeRepository DrinkTypeRepository { get; }
    IFoodRepository FoodRepository { get; }
    IFoodTypeRepository FoodTypeRepository { get; }
    IOrderRepository OrderRepository { get; }
    IFoodOrderRepository FoodOrderRepository { get; }
    IDrinkOrderRepository DrinkOrderRepository { get; }
    ITerminalRepository TerminalRepository { get; }
    IRefreshTokenRepository RefreshTokenRepository { get; }
    IUnitOfWork UnitOfWork { get; }
}
