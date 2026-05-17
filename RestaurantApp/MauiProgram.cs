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

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<RestaurantService>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();

            builder.Services.AddTransient<MyOrderPage>();
            builder.Services.AddTransient<MyOrderViewModel>();

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
