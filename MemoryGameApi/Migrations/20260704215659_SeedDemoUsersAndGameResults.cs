using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MemoryGameApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedDemoUsersAndGameResults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "demo1@example.com", "$2a$11$5XPr6Kjk/jJmse1wP71jpO06ZLsknX/zDDRhp4Q8S..hOhUSXgwLC", 0, "demo_player1" },
                    { 2, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "demo2@example.com", "$2a$11$5XPr6Kjk/jJmse1wP71jpO06ZLsknX/zDDRhp4Q8S..hOhUSXgwLC", 0, "demo_player2" }
                });

            migrationBuilder.InsertData(
                table: "GameResults",
                columns: new[] { "Id", "DifficultyId", "Moves", "PlayedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 6, new DateTime(2025, 6, 1, 10, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 2, 1, 8, new DateTime(2025, 6, 2, 10, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 3, 2, 14, new DateTime(2025, 6, 3, 10, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 4, 3, 20, new DateTime(2025, 6, 4, 10, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 5, 1, 5, new DateTime(2025, 6, 1, 12, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 6, 2, 12, new DateTime(2025, 6, 2, 12, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 7, 2, 10, new DateTime(2025, 6, 5, 12, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 8, 4, 35, new DateTime(2025, 6, 6, 12, 0, 0, 0, DateTimeKind.Utc), 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
