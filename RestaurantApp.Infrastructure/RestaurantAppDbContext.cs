using Microsoft.EntityFrameworkCore;
using RestaurantApp.Infrastructure.Models;

namespace RestaurantApp.Infrastructure;

public class RestaurantAppDbContext : DbContext
{
    public RestaurantAppDbContext(DbContextOptions<RestaurantAppDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FoodOrder>()
            .HasKey(fo => new { fo.FoodId, fo.OrderId });

        base.OnModelCreating(modelBuilder);
    }
}
