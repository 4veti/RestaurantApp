using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Infrastructure.Configuration;

internal class FoodTypeConfiguration : IEntityTypeConfiguration<FoodType>
{
    public void Configure(EntityTypeBuilder<FoodType> builder)
    {
        List<FoodType> terminals =
        [
            new FoodType()
            {
                Id = 1,
                Name = "Салати",
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            },
            new FoodType()
            {
                Id = 2,
                Name = "Предястия",
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            },
            new FoodType()
            {
                Id = 3,
                Name = "Основни",
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            },
            new FoodType()
            {
                Id = 4,
                Name = "Гарнитури",
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            },
            new FoodType()
            {
                Id = 5,
                Name = "Десерти",
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            }
        ];

        builder.HasData(terminals);
    }
}
