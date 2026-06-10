using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantApp.Domain.Entities;

namespace RestaurantApp.Infrastructure.Configuration;

internal class TerminalTypeConfiguration : IEntityTypeConfiguration<TerminalType>
{
    public void Configure(EntityTypeBuilder<TerminalType> builder)
    {
        List<TerminalType> terminals =
        [
            new TerminalType()
            {
                Id = 1,
                Name = "Cashier",
                Description = "An administrative terminal for handling payments and managing the menu."
            },
            new TerminalType()
            {
                Id = 2,
                Name = "Kitchen",
                Description = "A terminal for managing orders in a kitchen."
            },
            new TerminalType()
            {
                Id = 3,
                Name = "Client",
                Description = "A client terminal for making orders."
            }
        ];

        builder.HasData(terminals);
    }
}
