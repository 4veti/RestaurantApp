using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantApp.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace RestaurantApp.Infrastructure.Configuration;

internal class TerminalConfiguration : IEntityTypeConfiguration<Terminal>
{
    public void Configure(EntityTypeBuilder<Terminal> builder)
    {
        List<Terminal> terminals = GenerateTerminals();        

        builder.HasData(terminals);
    }

    private List<Terminal> GenerateTerminals()
    {
        List<Terminal> terminals =
        [
            new Terminal()
            {
                Id = 1,
                Description = "Main cashier terminal",
                TerminalTypeId = 1,
                HashedSecret = ComputeSha256Lowered("ad059a3c-7746-4f00-9c5a-bad4fff0ce20"),
                IsLockedOut = false
            },
            new Terminal()
            {
                Id = 2,
                Description = "Main kitchen terminal",
                TerminalTypeId = 2,
                HashedSecret = ComputeSha256Lowered("f867fc63-e2a2-4b3f-807a-d48ac1cfbc13"),
                IsLockedOut = false
            },
            new Terminal()
            {
                Id = 3,
                Description = "Main client terminal",
                TerminalTypeId = 3,
                HashedSecret = ComputeSha256Lowered("3ddbf723-d2a6-43e6-9d61-030958c6f5b5"),
                IsLockedOut = false
            }
        ];


        return terminals;
    }

    private static string ComputeSha256Lowered(string value)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(value);
        byte[] hash = SHA256.HashData(bytes);

        return Convert.ToHexString(hash).ToLower();
    }
}
