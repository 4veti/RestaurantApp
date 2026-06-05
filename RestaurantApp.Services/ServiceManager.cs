using Microsoft.Extensions.Options;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Services.Contracts;

namespace RestaurantApp.Services;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IFoodService> _lazyFoodService;
    private readonly Lazy<IDrinkService> _lazyDrinkService;
    private readonly Lazy<IOrderService> _lazyOrderService;
    private readonly Lazy<IFoodTypeService> _lazyFoodTypeService;
    private readonly Lazy<IDrinkTypeService> _lazyDrinkTypeService;
    private readonly Lazy<IAuthorizationService> _authorizationService;

    public ServiceManager(IRepositoryManager repositoryManager, IOptions<JwtOptions> authOptions, IOptions<TerminalSecrets> terminalSecrets)
    {
        _lazyFoodService = new Lazy<IFoodService>(() => new FoodService(repositoryManager));
        _lazyDrinkService = new Lazy<IDrinkService>(() => new DrinkService(repositoryManager));
        _lazyOrderService = new Lazy<IOrderService>(() => new OrderService(repositoryManager));
        _lazyFoodTypeService = new Lazy<IFoodTypeService>(() => new FoodTypeService(repositoryManager));
        _lazyDrinkTypeService = new Lazy<IDrinkTypeService>(() => new DrinkTypeService(repositoryManager));
        _authorizationService = new Lazy<IAuthorizationService>(() => new AuthorizationService(repositoryManager, authOptions, terminalSecrets));
    }

    public IFoodService FoodService => _lazyFoodService.Value;
    public IDrinkService DrinkService => _lazyDrinkService.Value;
    public IOrderService OrderService => _lazyOrderService.Value;
    public IFoodTypeService FoodTypeService => _lazyFoodTypeService.Value;
    public IDrinkTypeService DrinkTypeService => _lazyDrinkTypeService.Value;
    public IAuthorizationService AuthorizationService => _authorizationService.Value;
}
