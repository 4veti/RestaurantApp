namespace RestaurantApp.Services.Contracts;

public interface IServiceManager
{
    IFoodService FoodService { get; }
    IDrinkService DrinkService { get; }
    IOrderService OrderService { get; }
    IFoodTypeService FoodTypeService { get; }
    IDrinkTypeService DrinkTypeService { get; }
}
