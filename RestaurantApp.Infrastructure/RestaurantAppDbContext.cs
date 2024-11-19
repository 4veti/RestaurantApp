using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain.Entities;

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
    public DbSet<DrinkOrder> DrinksOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FoodOrder>()
            .HasKey(fo => new { fo.FoodId, fo.OrderId });

        modelBuilder.Entity<DrinkOrder>()
            .HasKey(dr => new { dr.DrinkId, dr.OrderId });

        base.OnModelCreating(modelBuilder);
    }
}
