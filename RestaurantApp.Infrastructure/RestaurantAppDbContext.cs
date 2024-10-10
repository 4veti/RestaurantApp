using Microsoft.EntityFrameworkCore;
using RestaurantApp.Infrastructure.Models;

namespace RestaurantApp.Infrastructure;

public class RestaurantAppDbContext : DbContext
{
    public RestaurantAppDbContext(DbContextOptions<RestaurantAppDbContext> options)
        : base(options)
    {

    }

    public DbSet<DrinkType> DrinkTypes { get; set; }
    public DbSet<Drink> Drinks { get; set; }
    public DbSet<FoodType> FoodTypes { get; set; }
    public DbSet<Food> Foods { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<FoodOrder> FoodsOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FoodOrder>()
            .HasKey(fo => new { fo.FoodId, fo.OrderId });

        base.OnModelCreating(modelBuilder);
    }
}
