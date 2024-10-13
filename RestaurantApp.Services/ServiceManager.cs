using RestaurantApp.Services.Abstractions;

namespace RestaurantApp.Services;

public sealed class ServiceManager
{
    private readonly Lazy<IFoodService> _lazyFoodService;
    private readonly Lazy<IDrinkService> _lazyDrinkService;
    private readonly Lazy<IOrderService> _lazyOrderService;
    private readonly Lazy<IFoodTypeService> _lazyFoodTypeService;
    private readonly Lazy<IDrinkTypeService> _lazyDrinkTypeService;

    public ServiceManager()
    {
        _lazyFoodService = new Lazy<IFoodService>(() => new FoodService());
        _lazyDrinkService = new Lazy<IDrinkService>(() => new DrinkService());
        _lazyOrderService = new Lazy<IOrderService>(() => new OrderService());
        _lazyFoodTypeService = new Lazy<IFoodTypeService>(() => new FoodTypeService());
        _lazyDrinkTypeService = new Lazy<IDrinkTypeService>(() => new DrinkTypeService());
    }

    public IFoodService FoodService => _lazyFoodService.Value;
    public IDrinkService drinkService => _lazyDrinkService.Value;
    public IOrderService OrderService => _lazyOrderService.Value;
    public IFoodTypeService FoodTypeService => _lazyFoodTypeService.Value;
    public IDrinkTypeService DrinkTypeService => _lazyDrinkTypeService.Value;
}
