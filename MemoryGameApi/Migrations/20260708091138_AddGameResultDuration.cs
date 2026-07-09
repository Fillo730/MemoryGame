using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemoryGameApi.Migrations
{
    /// <inheritdoc />
    public partial class AddGameResultDuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurationSeconds",
                table: "GameResults",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationSeconds",
                table: "GameResults");
        }
    }
}
