using Microsoft.EntityFrameworkCore;
using RestaurantApp.ClientApi.ExceptionHandlers;
using RestaurantApp.Infrastructure;
using RestaurantApp.Services;
using RestaurantApp.Services.Contracts;

namespace RestaurantApp.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

            builder.Services.AddDbContext<RestaurantAppDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddControllers();

            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseExceptionHandler();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}
