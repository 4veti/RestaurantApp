using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Infrastructure.Configuration;

internal class DrinkTypeConfiguration : IEntityTypeConfiguration<DrinkType>
{
    public void Configure(EntityTypeBuilder<DrinkType> builder)
    {
        List<DrinkType> terminals =
        [
            new DrinkType()
            {
                Id = 1,
                Name = "Топли",
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            },
            new DrinkType()
            {
                Id = 2,
                Name = "Студени",
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            },
            new DrinkType()
            {
                Id = 3,
                Name = "Коктейли",
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            },
            new DrinkType()
            {
                Id = 4,
                Name = "Вина",
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            },
            new DrinkType()
            {
                Id = 5,
                Name = "Бири",
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            }
        ];

        builder.HasData(terminals);
    }
}
