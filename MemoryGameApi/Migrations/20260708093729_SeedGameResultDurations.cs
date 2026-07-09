using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemoryGameApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedGameResultDurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 1,
                column: "DurationSeconds",
                value: 28);

            migrationBuilder.UpdateData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 2,
                column: "DurationSeconds",
                value: 37);

            migrationBuilder.UpdateData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 3,
                column: "DurationSeconds",
                value: 64);

            migrationBuilder.UpdateData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 4,
                column: "DurationSeconds",
                value: 95);

            migrationBuilder.UpdateData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 5,
                column: "DurationSeconds",
                value: 22);

            migrationBuilder.UpdateData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 6,
                column: "DurationSeconds",
                value: 54);

            migrationBuilder.UpdateData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 7,
                column: "DurationSeconds",
                value: 45);

            migrationBuilder.UpdateData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 8,
                column: "DurationSeconds",
                value: 168);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 1,
                column: "DurationSeconds",
                value: 0);

            migrationBuilder.UpdateData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 2,
                column: "DurationSeconds",
                value: 0);

            migrationBuilder.UpdateData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 3,
                column: "DurationSeconds",
                value: 0);

            migrationBuilder.UpdateData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 4,
                column: "DurationSeconds",
                value: 0);

            migrationBuilder.UpdateData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 5,
                column: "DurationSeconds",
                value: 0);

            migrationBuilder.UpdateData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 6,
                column: "DurationSeconds",
                value: 0);

            migrationBuilder.UpdateData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 7,
                column: "DurationSeconds",
                value: 0);

            migrationBuilder.UpdateData(
                table: "GameResults",
                keyColumn: "Id",
                keyValue: 8,
                column: "DurationSeconds",
                value: 0);
        }
    }
}
