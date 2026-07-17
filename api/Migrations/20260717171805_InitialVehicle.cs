using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobileDataTerminal.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlateNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MvFileNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Vin = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RegisterIssuanceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    RegisterExpiryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "fk_vehicles_licenses",
                        column: x => x.LicenseId,
                        principalTable: "Licenses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_LicenseId",
                table: "Vehicles",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "uq_vehicles_mv_file_number",
                table: "Vehicles",
                column: "MvFileNumber",
                unique: true,
                filter: "[MvFileNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "uq_vehicles_plate_number",
                table: "Vehicles",
                column: "PlateNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_vehicles_vin",
                table: "Vehicles",
                column: "Vin",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
