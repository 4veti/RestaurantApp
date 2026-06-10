using RestaurantApp.Services;
using RestaurantApp.ViewModels;
using RestaurantApp.Views;
using System.Diagnostics;
using static RestaurantApp.Utils.Constants;

namespace RestaurantApp;

public partial class App : Application
{
    private readonly MainPageViewModel _mainPageViewModel;
    private readonly KitchenOrdersPageViewModel _kitchenOrdersPageViewModel;
    private readonly FrontOfficeViewModel _frontOfficeViewModel;
    private readonly AuthService _authService;

    public App(MainPageViewModel mainPageViewModel,
            KitchenOrdersPageViewModel kitchenOrdersPageViewModel,
            FrontOfficeViewModel frontOfficeViewModel,
            AuthService authService)
    {
        _mainPageViewModel = mainPageViewModel;
        _kitchenOrdersPageViewModel = kitchenOrdersPageViewModel;
        _frontOfficeViewModel = frontOfficeViewModel;
        _authService = authService;

        InitializeComponent();

        MainPage = new LoadingPage();
    }

    protected override async void OnStart()
    {
        base.OnStart();

        await InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        try
        {
#if DEBUG
            SecureStorage.RemoveAll();
#endif

            string refreshToken = await SecureStorage.GetAsync(RefreshTokenKey) ?? string.Empty;
            int terminalType;

            if (string.IsNullOrWhiteSpace(refreshToken) == false)
            {
                (bool refreshed, terminalType) = await _authService.TryRefreshAsync();
                if (refreshed)
                {
                    MainPage = new AppShell(_mainPageViewModel, _kitchenOrdersPageViewModel, _frontOfficeViewModel, terminalType);
                    return;
                }
            }

            terminalType = await _authService.LoginAsync();

            if (terminalType < 1)
            {
                MainPage = new FailedConnectionPage();
                return;
            }

            MainPage = new AppShell(_mainPageViewModel, _kitchenOrdersPageViewModel, _frontOfficeViewModel, terminalType);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An exception has occurred during App Initialization: {ex.Message}");
        }
    }
}
