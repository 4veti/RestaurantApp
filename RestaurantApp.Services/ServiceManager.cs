using RestaurantApp.Services.Contracts;

namespace RestaurantApp.Services;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IFoodService> _lazyFoodService;
    private readonly Lazy<IDrinkService> _lazyDrinkService;
    private readonly Lazy<IOrderService> _lazyOrderService;
    private readonly Lazy<IFoodTypeService> _lazyFoodTypeService;
    private readonly Lazy<IDrinkTypeService> _lazyDrinkTypeService;

    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _lazyFoodService = new Lazy<IFoodService>(() => new FoodService(repositoryManager));
        _lazyDrinkService = new Lazy<IDrinkService>(() => new DrinkService(repositoryManager));
        _lazyOrderService = new Lazy<IOrderService>(() => new OrderService(repositoryManager));
        _lazyFoodTypeService = new Lazy<IFoodTypeService>(() => new FoodTypeService(repositoryManager));
        _lazyDrinkTypeService = new Lazy<IDrinkTypeService>(() => new DrinkTypeService(repositoryManager));
    }

    public IFoodService FoodService => _lazyFoodService.Value;
    public IDrinkService DrinkService => _lazyDrinkService.Value;
    public IOrderService OrderService => _lazyOrderService.Value;
    public IFoodTypeService FoodTypeService => _lazyFoodTypeService.Value;
    public IDrinkTypeService DrinkTypeService => _lazyDrinkTypeService.Value;
}
