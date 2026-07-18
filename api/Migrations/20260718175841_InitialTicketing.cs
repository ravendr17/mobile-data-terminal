using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MobileDataTerminal.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialTicketing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Violations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsTiered = table.Column<bool>(type: "bit", nullable: false),
                    InitialFine = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    SecondFine = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    ThirdFine = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Violations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseId = table.Column<int>(type: "int", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    SettledAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    IncidentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IncidentPlace = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    OfficerNotes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_TicketStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "TicketStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_tickets_licenses",
                        column: x => x.LicenseId,
                        principalTable: "Licenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketViolations",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    ViolationId = table.Column<int>(type: "int", nullable: false),
                    Fine = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketViolations", x => new { x.TicketId, x.ViolationId });
                    table.ForeignKey(
                        name: "fk_ticket_violations_tickets",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ticket_violations_violations",
                        column: x => x.ViolationId,
                        principalTable: "Violations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TicketStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Unsettled" },
                    { 2, "Settled" }
                });

            migrationBuilder.InsertData(
                table: "Violations",
                columns: new[] { "Id", "InitialFine", "IsTiered", "Name", "SecondFine", "ThirdFine" },
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

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_LicenseId",
                table: "Tickets",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_StatusId",
                table: "Tickets",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "uq_tickets_reference_number",
                table: "Tickets",
                column: "ReferenceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_ticket_statuses_name",
                table: "TicketStatuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketViolations_ViolationId",
                table: "TicketViolations",
                column: "ViolationId");

            migrationBuilder.CreateIndex(
                name: "uq_violations_name",
                table: "Violations",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketViolations");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Violations");

            migrationBuilder.DropTable(
                name: "TicketStatuses");
        }
    }
}
