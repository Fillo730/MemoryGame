using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MemoryGameApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Difficulties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumberOfPairs = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DifficultyTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DifficultyId = table.Column<int>(type: "INTEGER", nullable: false),
                    LanguageCode = table.Column<string>(type: "TEXT", nullable: false),
                    Label = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DifficultyTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DifficultyTranslations_Difficulties_DifficultyId",
                        column: x => x.DifficultyId,
                        principalTable: "Difficulties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Moves = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    DifficultyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameResults_Difficulties_DifficultyId",
                        column: x => x.DifficultyId,
                        principalTable: "Difficulties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameResults_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "NumberOfPairs" },
                values: new object[,]
                {
                    { 1, 4 },
                    { 2, 6 },
                    { 3, 8 },
                    { 4, 10 },
                    { 5, 12 },
                    { 6, 15 },
                    { 7, 18 },
                    { 8, 21 },
                    { 9, 25 }
                });

            migrationBuilder.InsertData(
                table: "DifficultyTranslations",
                columns: new[] { "Id", "DifficultyId", "Label", "LanguageCode" },
                values: new object[,]
                {
                    { 1, 1, "Easy", "en" },
                    { 2, 1, "Facile", "it" },
                    { 3, 2, "Medium", "en" },
                    { 4, 2, "Medio", "it" },
                    { 5, 3, "Hard", "en" },
                    { 6, 3, "Difficile", "it" },
                    { 7, 4, "Extreme", "en" },
                    { 8, 4, "Estremo", "it" },
                    { 9, 5, "Impossible", "en" },
                    { 10, 5, "Impossibile", "it" },
                    { 11, 6, "Legendary", "en" },
                    { 12, 6, "Leggendario", "it" },
                    { 13, 7, "Mythical", "en" },
                    { 14, 7, "Mitico", "it" },
                    { 15, 8, "Divine", "en" },
                    { 16, 8, "Divino", "it" },
                    { 17, 9, "Godlike", "en" },
                    { 18, 9, "Sovrumano", "it" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DifficultyTranslations_DifficultyId",
                table: "DifficultyTranslations",
                column: "DifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_GameResults_DifficultyId",
                table: "GameResults",
                column: "DifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_GameResults_UserId",
                table: "GameResults",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DifficultyTranslations");

            migrationBuilder.DropTable(
                name: "GameResults");

            migrationBuilder.DropTable(
                name: "Difficulties");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
