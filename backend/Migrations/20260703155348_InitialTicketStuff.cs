using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialTicketStuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    license_id = table.Column<int>(type: "integer", nullable: false),
                    reference_number = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    settled_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    incident_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    incident_place = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    officer_notes = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ticket", x => x.id);
                    table.ForeignKey(
                        name: "fk_tickets_licenses",
                        column: x => x.license_id,
                        principalTable: "licenses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "violations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_tiered = table.Column<bool>(type: "boolean", nullable: false),
                    initial_fine = table.Column<decimal>(type: "numeric", nullable: false),
                    second_fine = table.Column<decimal>(type: "numeric", nullable: true),
                    third_fine = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_violations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ticket_violation",
                columns: table => new
                {
                    ticket_id = table.Column<int>(type: "integer", nullable: false),
                    violation_id = table.Column<int>(type: "integer", nullable: false),
                    fine_charged = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ticket_violation", x => new { x.ticket_id, x.violation_id });
                    table.ForeignKey(
                        name: "fk_ticket_violations_tickets",
                        column: x => x.ticket_id,
                        principalTable: "ticket",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ticket_violations_violations",
                        column: x => x.violation_id,
                        principalTable: "violations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_ticket_license_id",
                table: "ticket",
                column: "license_id");

            migrationBuilder.CreateIndex(
                name: "uq_tickets_reference_number",
                table: "ticket",
                column: "reference_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ticket_violation_violation_id",
                table: "ticket_violation",
                column: "violation_id");

            migrationBuilder.CreateIndex(
                name: "uq_violations_name",
                table: "violations",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ticket_violation");

            migrationBuilder.DropTable(
                name: "ticket");

            migrationBuilder.DropTable(
                name: "violations");
        }
    }
}
