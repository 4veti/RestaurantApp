using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestaurantApp.Models;
using RestaurantApp.Services;
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


            builder.Configuration.AddJsonFile("appsettings.json", optional: false);

            builder.Services.Configure<ApplicationSettings>(
                    builder.Configuration.GetSection("ApplicationSettings"));

            ApplicationSettings settings = builder.Configuration
                .GetSection("ApplicationSettings")
                .Get<ApplicationSettings>() ?? throw new InvalidOperationException("Missing ApplicationSettings in configuration"); ;

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<RestaurantService>();

            //if (settings.RunMode == RunMode.Client)
            {
                builder.Services.AddSingleton<MainPage>();
                builder.Services.AddTransient<MyOrderPage>();

                builder.Services.AddSingleton<MainPageViewModel>();
                builder.Services.AddTransient<MyOrderViewModel>();
            }

            //if (settings.RunMode == RunMode.Kitchen)
            {
                builder.Services.AddSingleton<KitchenOrdersPage>();
                builder.Services.AddSingleton<KitchenOrdersPageViewModel>();
            }

            //if (settings.RunMode == RunMode.FrontDesk)
            {
                builder.Services.AddSingleton<FrontOfficeViewModel>();

                builder.Services.AddSingleton<FrontOfficeOrdersPage>();
                builder.Services.AddSingleton<FrontOfficeFoodsPage>();
                builder.Services.AddSingleton<FrontOfficeDrinksPage>();

                builder.Services.AddSingleton<KitchenOrdersPageViewModel>();
            }

            return builder.Build();
        }
    }
}
