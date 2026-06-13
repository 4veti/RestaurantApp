using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestaurantApp.Models;
using RestaurantApp.Services;
using RestaurantApp.Utils;
using RestaurantApp.ViewModels;
using RestaurantApp.Views;

namespace RestaurantApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            string config = "appsettings.json";
            string[] args = Environment.GetCommandLineArgs();

            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i] == "--config")
                {
                    config = args[i + 1];
                    break;
                }
            }

            builder.Configuration.AddJsonFile(config, optional: false);

            builder.Services.Configure<ApplicationSettings>(
                    builder.Configuration.GetSection("ApplicationSettings"));

            builder.Services.AddSingleton<AuthHandler>();
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<TokenStore>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<RestaurantService>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();

            builder.Services.AddSingleton<KitchenOrdersPage>();
            builder.Services.AddSingleton<KitchenOrdersPageViewModel>();

            builder.Services.AddSingleton<FrontOfficeOrdersPage>();
            builder.Services.AddSingleton<FrontOfficeFoodsPage>();
            builder.Services.AddSingleton<FrontOfficeDrinksPage>();
            builder.Services.AddSingleton<FrontOfficeViewModel>();

            return builder.Build();
        }
    }
}
