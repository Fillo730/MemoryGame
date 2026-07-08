using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MemoryGameApi.Migrations
{
    /// <inheritdoc />
    public partial class AddFrenchAndGermanTranslations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AchievementTranslations",
                columns: new[] { "Id", "AchievementId", "Description", "LanguageCode", "Name" },
                values: new object[,]
                {
                    { 17, 1, "Termine ta première partie.", "fr", "Premiers Pas" },
                    { 18, 1, "Schließe dein erstes Spiel ab.", "de", "Erste Schritte" },
                    { 19, 2, "Joue 10 parties.", "fr", "Habitué" },
                    { 20, 2, "Spiele 10 Partien.", "de", "Stammspieler" },
                    { 21, 3, "Joue 50 parties.", "fr", "Vétéran" },
                    { 22, 3, "Spiele 50 Partien.", "de", "Veteran" },
                    { 23, 4, "Joue 100 parties.", "fr", "Légende" },
                    { 24, 4, "Spiele 100 Partien.", "de", "Legende" },
                    { 25, 5, "Termine une partie sans la moindre erreur.", "fr", "Mémoire de Fer" },
                    { 26, 5, "Schließe eine Partie ohne einen einzigen Fehlversuch ab.", "de", "Eisernes Gedächtnis" },
                    { 27, 6, "Gagne au moins une partie à chaque niveau de difficulté.", "fr", "Explorateur" },
                    { 28, 6, "Gewinne mindestens eine Partie auf jedem Schwierigkeitsgrad.", "de", "Entdecker" },
                    { 29, 7, "Gagne une partie au niveau de difficulté le plus élevé.", "fr", "Surhumain" },
                    { 30, 7, "Gewinne eine Partie auf dem höchsten Schwierigkeitsgrad.", "de", "Übermenschlich" },
                    { 31, 8, "Termine une partie entre minuit et 5 heures du matin.", "fr", "Oiseau de Nuit" },
                    { 32, 8, "Schließe eine Partie zwischen Mitternacht und 5 Uhr morgens ab.", "de", "Nachteule" }
                });

            migrationBuilder.InsertData(
                table: "DifficultyTranslations",
                columns: new[] { "Id", "DifficultyId", "Label", "LanguageCode" },
                values: new object[,]
                {
                    { 19, 1, "Facile", "fr" },
                    { 20, 1, "Einfach", "de" },
                    { 21, 2, "Moyen", "fr" },
                    { 22, 2, "Mittel", "de" },
                    { 23, 3, "Difficile", "fr" },
                    { 24, 3, "Schwer", "de" },
                    { 25, 4, "Extrême", "fr" },
                    { 26, 4, "Extrem", "de" },
                    { 27, 5, "Impossible", "fr" },
                    { 28, 5, "Unmöglich", "de" },
                    { 29, 6, "Légendaire", "fr" },
                    { 30, 6, "Legendär", "de" },
                    { 31, 7, "Mythique", "fr" },
                    { 32, 7, "Mythisch", "de" },
                    { 33, 8, "Divin", "fr" },
                    { 34, 8, "Göttlich", "de" },
                    { 35, 9, "Surhumain", "fr" },
                    { 36, 9, "Übermenschlich", "de" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "AchievementTranslations",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "DifficultyTranslations",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "DifficultyTranslations",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "DifficultyTranslations",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "DifficultyTranslations",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "DifficultyTranslations",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "DifficultyTranslations",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "DifficultyTranslations",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "DifficultyTranslations",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "DifficultyTranslations",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "DifficultyTranslations",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "DifficultyTranslations",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "DifficultyTranslations",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "DifficultyTranslations",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "DifficultyTranslations",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "DifficultyTranslations",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "DifficultyTranslations",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "DifficultyTranslations",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "DifficultyTranslations",
                keyColumn: "Id",
                keyValue: 36);
        }
    }
}
