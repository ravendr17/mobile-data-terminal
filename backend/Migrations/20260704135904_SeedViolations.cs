using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedViolations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "violations",
                columns: new[] { "id", "initial_fine", "is_tiered", "name", "second_fine", "third_fine" },
                values: new object[,]
                {
                    { 1, 3000m, false, "DRIVING WITHOUT VALID LICENSE", null, null },
                    { 2, 1000m, false, "FAILURE TO CARRY LICENSE", null, null },
                    { 3, 3000m, false, "FAKE DRIVER'S LICENSE", null, null },
                    { 4, 10000m, false, "DRIVING UNREGISTERED VEHICLE", null, null },
                    { 5, 5000m, false, "ILLEGAL MODIFICATIONS", null, null },
                    { 6, 5000m, false, "DEFECTIVE/IMPROPER EQUIPMENT", null, null },
                    { 7, 2000m, true, "RECKLESS DRIVING", 3000m, 10000m },
                    { 8, 1000m, true, "NO SEATBELT", 3000m, 5000m },
                    { 9, 1500m, true, "NO HELMET", 3000m, 5000m },
                    { 10, 20000m, true, "DRIVING UNDER INFLUENCE", 50000m, 100000m },
                    { 11, 1000m, false, "OBSTRUCTION", null, null },
                    { 12, 1000m, false, "NO OR/CR", null, null },
                    { 13, 2000m, false, "OVERLOADING PASSENGERS", null, null },
                    { 14, 1000m, false, "OVER-SPEEDING", null, null },
                    { 15, 1000m, false, "BEATING THE RED LIGHT", null, null },
                    { 16, 1000m, false, "ILLEGAL PARKING", null, null },
                    { 17, 1000m, false, "USING PHONE WHILE DRIVING", null, null },
                    { 18, 2000m, false, "COUNTERFLOWING", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "violations",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "violations",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "violations",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "violations",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "violations",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "violations",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "violations",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "violations",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "violations",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "violations",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "violations",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "violations",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "violations",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "violations",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "violations",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "violations",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "violations",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "violations",
                keyColumn: "id",
                keyValue: 18);
        }
    }
}
