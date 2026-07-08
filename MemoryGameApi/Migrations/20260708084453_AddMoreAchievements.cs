using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MemoryGameApi.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreAchievements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "Code", "Icon" },
                values: new object[,]
                {
                    { 9, "FIVE_HUNDRED_GAMES", "diamond" },
                    { 10, "EARLY_BIRD", "wb_twilight" },
                    { 11, "WEEKEND_WARRIOR", "weekend" },
                    { 12, "PERFECT_STREAK", "auto_awesome" },
                    { 13, "MARATHON_DAY", "directions_run" },
                    { 14, "FLAWLESS_LEGEND", "verified" }
                });

            migrationBuilder.InsertData(
                table: "AchievementTranslations",
                columns: new[] { "Id", "AchievementId", "Description", "LanguageCode", "Name" },
                values: new object[,]
                {
                    { 33, 9, "Play 500 games.", "en", "Unstoppable" },
                    { 34, 9, "Gioca 500 partite.", "it", "Inarrestabile" },
                    { 35, 9, "Joue 500 parties.", "fr", "Inarrêtable" },
                    { 36, 9, "Spiele 500 Partien.", "de", "Unaufhaltsam" },
                    { 37, 10, "Complete a game between 5 AM and 8 AM.", "en", "Early Bird" },
                    { 38, 10, "Completa una partita tra le 5 e le 8 del mattino.", "it", "Mattiniero" },
                    { 39, 10, "Termine une partie entre 5 h et 8 h du matin.", "fr", "Lève-tôt" },
                    { 40, 10, "Schließe eine Partie zwischen 5 und 8 Uhr morgens ab.", "de", "Frühaufsteher" },
                    { 41, 11, "Play a game on Saturday or Sunday.", "en", "Weekend Warrior" },
                    { 42, 11, "Gioca una partita di sabato o domenica.", "it", "Guerriero del Weekend" },
                    { 43, 11, "Joue une partie le samedi ou le dimanche.", "fr", "Guerrier du Week-end" },
                    { 44, 11, "Spiele eine Partie am Samstag oder Sonntag.", "de", "Wochenendkrieger" },
                    { 45, 12, "Complete 3 flawless games.", "en", "Perfectionist" },
                    { 46, 12, "Completa 3 partite senza errori.", "it", "Perfezionista" },
                    { 47, 12, "Termine 3 parties sans la moindre erreur.", "fr", "Perfectionniste" },
                    { 48, 12, "Schließe 3 fehlerfreie Partien ab.", "de", "Perfektionist" },
                    { 49, 13, "Play 5 games in a single day.", "en", "Marathon Runner" },
                    { 50, 13, "Gioca 5 partite in un solo giorno.", "it", "Maratoneta" },
                    { 51, 13, "Joue 5 parties en une seule journée.", "fr", "Marathonien" },
                    { 52, 13, "Spiele 5 Partien an einem einzigen Tag.", "de", "Marathonläufer" },
                    { 53, 14, "Complete the hardest difficulty flawlessly.", "en", "Flawless Legend" },
                    { 54, 14, "Completa la difficoltà più alta senza errori.", "it", "Leggenda Perfetta" },
                    { 55, 14, "Termine le niveau de difficulté le plus élevé sans la moindre erreur.", "fr", "Légende Parfaite" },
                    { 56, 14, "Schließe den höchsten Schwierigkeitsgrad fehlerfrei ab.", "de", "Makellose Legende" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 14);
        }
    }
}
