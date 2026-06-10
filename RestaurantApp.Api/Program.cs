using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantApp.ClientApi.ExceptionHandlers;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Infrastructure;
using RestaurantApp.Services;
using RestaurantApp.Services.Contracts;
using System.Text;

namespace RestaurantApp.ClientApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString") ?? string.Empty;

            builder.Services.AddDbContext<RestaurantAppDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddControllers();

            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            builder.Configuration.AddJsonFile("appsettings.Development.json", optional: false);

            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningSecret"]!)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    };
                });

            builder.Services.Configure<JwtOptions>(
                builder.Configuration.GetSection("Jwt"));

            builder.Services.Configure<TerminalSecrets>(
                builder.Configuration.GetSection("TerminalSecrets"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseExceptionHandler();

            app.MapControllers();

            //if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                //app.UseSwaggerUI(c =>
                //{
                //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPIV1");
                //});
            }

            await app.RunAsync();
        }
    }
}
