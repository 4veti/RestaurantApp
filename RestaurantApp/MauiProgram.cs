using Microsoft.Extensions.Logging;
using RestaurantApp.Services;
using RestaurantApp.ViewModels;

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

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<RestaurantService>();

            builder.Services.AddSingleton<FoodsViewModel>();
            builder.Services.AddSingleton<DrinksViewModel>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<DrinksPage>();

            return builder.Build();
        }
    }
}
