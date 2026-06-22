using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedAccountRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "account_roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Civilian" },
                    { 2, "Officer" },
                    { 3, "Supervisor" },
                    { 4, "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "account_roles",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "account_roles",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "account_roles",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "account_roles",
                keyColumn: "id",
                keyValue: 4);
        }
    }
}
