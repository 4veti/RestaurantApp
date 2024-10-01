using Microsoft.EntityFrameworkCore;

namespace RestaurantApp.Infrastructure;

public class RestaurantAppDbContext : DbContext
{
    public RestaurantAppDbContext(DbContextOptions<RestaurantAppDbContext> options)
        : base(options)
    {

    }
}
