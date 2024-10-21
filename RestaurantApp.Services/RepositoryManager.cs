using RestaurantApp.Domain.Contracts.Repositories;
using RestaurantApp.Infrastructure.Repositories;
using RestaurantApp.Services.Abstractions;

namespace RestaurantApp.Services;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IDrinkRepository> _lazyDrinkRepository;
    private readonly Lazy<IDrinkTypeRepository> _lazyDrinkTypeRepository;
    private readonly Lazy<IFoodRepository> _lazyFoodRepository;
    private readonly Lazy<IFoodTypeRepository> _lazyFoodTypeRepository;
    private readonly Lazy<IOrderRepository> _lazyOrderRepository;
    private readonly Lazy<IFoodOrderRepository> _lazyFoodOrderRepository;
    private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

    public RepositoryManager()
    {
        _lazyDrinkRepository = new Lazy<IDrinkRepository>(() => new DrinkRepository());
        _lazyDrinkTypeRepository = new Lazy<IDrinkTypeRepository>(() => new DrinkTypeRepository());
        _lazyFoodRepository = new Lazy<IFoodRepository>(() => new FoodRepository());
        _lazyFoodTypeRepository = new Lazy<IFoodTypeRepository>(() => new FoodTypeRepository());
        _lazyOrderRepository = new Lazy<IOrderRepository>(() => new OrderRepository());
        _lazyFoodOrderRepository = new Lazy<IFoodOrderRepository>(() => new FoodOrderRepository());
        _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork());
    }

    public IDrinkRepository DrinkRepository => _lazyDrinkRepository.Value;
    public IDrinkTypeRepository DrinkTypeRepository => _lazyDrinkTypeRepository.Value;
    public IFoodRepository FoodRepository => _lazyFoodRepository.Value;
    public IFoodTypeRepository FoodTypeRepository => _lazyFoodTypeRepository.Value;
    public IOrderRepository OrderRepository => _lazyOrderRepository.Value;
    public IFoodOrderRepository FoodOrderRepository => _lazyFoodOrderRepository.Value;
    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
}
