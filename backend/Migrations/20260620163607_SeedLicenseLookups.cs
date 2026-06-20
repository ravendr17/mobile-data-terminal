using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedLicenseLookups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "license_statuses",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Revoked" },
                    { 3, "Expired" }
                });

            migrationBuilder.InsertData(
                table: "license_types",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Non-Professional" },
                    { 2, "Professional" },
                    { 3, "Student Permit" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "license_statuses",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "license_statuses",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "license_statuses",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "license_types",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "license_types",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "license_types",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
