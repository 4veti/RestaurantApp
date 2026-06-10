using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeededDemoData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DrinkTypes",
                columns: new[] { "Id", "Created", "Modified", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 6, 10, 20, 4, 20, 384, DateTimeKind.Utc).AddTicks(782), new DateTime(2026, 6, 10, 20, 4, 20, 384, DateTimeKind.Utc).AddTicks(783), "Топли" },
                    { 2, new DateTime(2026, 6, 10, 20, 4, 20, 384, DateTimeKind.Utc).AddTicks(784), new DateTime(2026, 6, 10, 20, 4, 20, 384, DateTimeKind.Utc).AddTicks(785), "Студени" },
                    { 3, new DateTime(2026, 6, 10, 20, 4, 20, 384, DateTimeKind.Utc).AddTicks(785), new DateTime(2026, 6, 10, 20, 4, 20, 384, DateTimeKind.Utc).AddTicks(786), "Коктейли" },
                    { 4, new DateTime(2026, 6, 10, 20, 4, 20, 384, DateTimeKind.Utc).AddTicks(786), new DateTime(2026, 6, 10, 20, 4, 20, 384, DateTimeKind.Utc).AddTicks(787), "Вина" },
                    { 5, new DateTime(2026, 6, 10, 20, 4, 20, 384, DateTimeKind.Utc).AddTicks(787), new DateTime(2026, 6, 10, 20, 4, 20, 384, DateTimeKind.Utc).AddTicks(787), "Бири" }
                });

            migrationBuilder.InsertData(
                table: "FoodTypes",
                columns: new[] { "Id", "Created", "Modified", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 6, 10, 20, 4, 20, 384, DateTimeKind.Utc).AddTicks(680), new DateTime(2026, 6, 10, 20, 4, 20, 384, DateTimeKind.Utc).AddTicks(681), "Салати" },
                    { 2, new DateTime(2026, 6, 10, 20, 4, 20, 384, DateTimeKind.Utc).AddTicks(682), new DateTime(2026, 6, 10, 20, 4, 20, 384, DateTimeKind.Utc).AddTicks(682), "Предястия" },
                    { 3, new DateTime(2026, 6, 10, 20, 4, 20, 384, DateTimeKind.Utc).AddTicks(683), new DateTime(2026, 6, 10, 20, 4, 20, 384, DateTimeKind.Utc).AddTicks(683), "Основни" },
                    { 4, new DateTime(2026, 6, 10, 20, 4, 20, 384, DateTimeKind.Utc).AddTicks(684), new DateTime(2026, 6, 10, 20, 4, 20, 384, DateTimeKind.Utc).AddTicks(684), "Гарнитури" },
                    { 5, new DateTime(2026, 6, 10, 20, 4, 20, 384, DateTimeKind.Utc).AddTicks(685), new DateTime(2026, 6, 10, 20, 4, 20, 384, DateTimeKind.Utc).AddTicks(685), "Десерти" }
                });

            migrationBuilder.InsertData(
                table: "TerminalTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "An administrative terminal for handling payments and managing the menu.", "Cashier" },
                    { 2, "A terminal for managing orders in a kitchen.", "Kitchen" },
                    { 3, "A client terminal for making orders.", "Client" }
                });

            migrationBuilder.InsertData(
                table: "Terminals",
                columns: new[] { "Id", "Description", "HashedSecret", "IsLockedOut", "TerminalTypeId" },
                values: new object[,]
                {
                    { 1, "Main cashier terminal", "3b433ce50731ea6c64d1bf7b35f3f741a300c5ba4feb81fdfcbf248d09a318aa", false, 1 },
                    { 2, "Main kitchen terminal", "3500d04ae3acd5b19ac48edfaa1b1e7dba48b7f3a5c2891c063c6a549957f8f5", false, 2 },
                    { 3, "Main client terminal", "e8ddf15adc116024cf009d842546a6a3f307e9fac04f7721d58b8baf9f416cac", false, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DrinkTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DrinkTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DrinkTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DrinkTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DrinkTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FoodTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FoodTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FoodTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FoodTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FoodTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Terminals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Terminals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Terminals",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TerminalTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TerminalTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TerminalTypes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
