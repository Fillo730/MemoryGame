using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MemoryGameApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAchievements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Icon = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AchievementTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AchievementId = table.Column<int>(type: "INTEGER", nullable: false),
                    LanguageCode = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AchievementTranslations_Achievements_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAchievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    AchievementId = table.Column<int>(type: "INTEGER", nullable: false),
                    UnlockedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAchievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAchievements_Achievements_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAchievements_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "Code", "Icon" },
                values: new object[,]
                {
                    { 1, "FIRST_WIN", "flag" },
                    { 2, "TEN_GAMES", "local_fire_department" },
                    { 3, "FIFTY_GAMES", "military_tech" },
                    { 4, "HUNDRED_GAMES", "workspace_premium" },
                    { 5, "FLAWLESS", "bolt" },
                    { 6, "ALL_DIFFICULTIES", "travel_explore" },
                    { 7, "HARDEST_DIFFICULTY", "emoji_events" },
                    { 8, "NIGHT_OWL", "nightlight" }
                });

            migrationBuilder.InsertData(
                table: "AchievementTranslations",
                columns: new[] { "Id", "AchievementId", "Description", "LanguageCode", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Complete your first game.", "en", "First Steps" },
                    { 2, 1, "Completa la tua prima partita.", "it", "Primi Passi" },
                    { 3, 2, "Play 10 games.", "en", "Regular" },
                    { 4, 2, "Gioca 10 partite.", "it", "Habitué" },
                    { 5, 3, "Play 50 games.", "en", "Veteran" },
                    { 6, 3, "Gioca 50 partite.", "it", "Veterano" },
                    { 7, 4, "Play 100 games.", "en", "Legend" },
                    { 8, 4, "Gioca 100 partite.", "it", "Leggenda" },
                    { 9, 5, "Complete a game without a single wrong guess.", "en", "Iron Memory" },
                    { 10, 5, "Completa una partita senza sbagliare nemmeno una mossa.", "it", "Memoria di Ferro" },
                    { 11, 6, "Win at least one game on every difficulty.", "en", "Explorer" },
                    { 12, 6, "Vinci almeno una partita per ogni difficoltà.", "it", "Esploratore" },
                    { 13, 7, "Win a game on the hardest difficulty.", "en", "Godlike" },
                    { 14, 7, "Vinci una partita nella difficoltà più alta.", "it", "Sovrumano" },
                    { 15, 8, "Complete a game between midnight and 5 AM.", "en", "Night Owl" },
                    { 16, 8, "Completa una partita tra mezzanotte e le 5 del mattino.", "it", "Nottambulo" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AchievementTranslations_AchievementId",
                table: "AchievementTranslations",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievements_AchievementId",
                table: "UserAchievements",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievements_UserId",
                table: "UserAchievements",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AchievementTranslations");

            migrationBuilder.DropTable(
                name: "UserAchievements");

            migrationBuilder.DropTable(
                name: "Achievements");
        }
    }
}
