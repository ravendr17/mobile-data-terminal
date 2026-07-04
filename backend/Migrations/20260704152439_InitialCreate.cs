using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "account_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_account_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "license_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_license_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "license_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_license_types", x => x.id);
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
                name: "licenses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    type_id = table.Column<int>(type: "integer", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    issuance_date = table.Column<DateOnly>(type: "date", nullable: false),
                    expiration_date = table.Column<DateOnly>(type: "date", nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    middle_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    sex = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    nationality = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    eye_color = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    height = table.Column<int>(type: "integer", nullable: false),
                    weight = table.Column<int>(type: "integer", nullable: false),
                    blood_type = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_licenses", x => x.id);
                    table.ForeignKey(
                        name: "fk_licenses_license_statuses",
                        column: x => x.status_id,
                        principalTable: "license_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_licenses_license_types",
                        column: x => x.type_id,
                        principalTable: "license_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    license_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_accounts", x => x.id);
                    table.ForeignKey(
                        name: "fk_accounts_account_roles",
                        column: x => x.role_id,
                        principalTable: "account_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_accounts_licenses",
                        column: x => x.license_id,
                        principalTable: "licenses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    license_id = table.Column<int>(type: "integer", nullable: false),
                    reference_number = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'"),
                    settled_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    incident_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    incident_place = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    officer_notes = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tickets", x => x.id);
                    table.ForeignKey(
                        name: "fk_tickets_licenses",
                        column: x => x.license_id,
                        principalTable: "licenses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vehicles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    plate_number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    mv_file_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    vin = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    register_issuance_date = table.Column<DateOnly>(type: "date", nullable: false),
                    register_expiry_date = table.Column<DateOnly>(type: "date", nullable: false),
                    make = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    model = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    color = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    license_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vehicles", x => x.id);
                    table.ForeignKey(
                        name: "fk_vehicles_licenses",
                        column: x => x.license_id,
                        principalTable: "licenses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ticket_violations",
                columns: table => new
                {
                    ticket_id = table.Column<int>(type: "integer", nullable: false),
                    violation_id = table.Column<int>(type: "integer", nullable: false),
                    fine_charged = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ticket_violations", x => new { x.ticket_id, x.violation_id });
                    table.ForeignKey(
                        name: "fk_ticket_violations_tickets",
                        column: x => x.ticket_id,
                        principalTable: "tickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ticket_violations_violations",
                        column: x => x.violation_id,
                        principalTable: "violations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "uq_account_roles_name",
                table: "account_roles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_accounts_license_id",
                table: "accounts",
                column: "license_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_accounts_role_id",
                table: "accounts",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "uq_accounts_email",
                table: "accounts",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_accounts_username",
                table: "accounts",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_license_statuses_name",
                table: "license_statuses",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_license_types_name",
                table: "license_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_licenses_status_id",
                table: "licenses",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_licenses_type_id",
                table: "licenses",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "uq_licenses_number",
                table: "licenses",
                column: "number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ticket_violations_violation_id",
                table: "ticket_violations",
                column: "violation_id");

            migrationBuilder.CreateIndex(
                name: "ix_tickets_license_id",
                table: "tickets",
                column: "license_id");

            migrationBuilder.CreateIndex(
                name: "uq_tickets_reference_number",
                table: "tickets",
                column: "reference_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_vehicles_license_id",
                table: "vehicles",
                column: "license_id");

            migrationBuilder.CreateIndex(
                name: "uq_vehicles_mv_file_number",
                table: "vehicles",
                column: "mv_file_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_vehicles_plate_number",
                table: "vehicles",
                column: "plate_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_vehicles_vin",
                table: "vehicles",
                column: "vin",
                unique: true);

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
                name: "accounts");

            migrationBuilder.DropTable(
                name: "ticket_violations");

            migrationBuilder.DropTable(
                name: "vehicles");

            migrationBuilder.DropTable(
                name: "account_roles");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "violations");

            migrationBuilder.DropTable(
                name: "licenses");

            migrationBuilder.DropTable(
                name: "license_statuses");

            migrationBuilder.DropTable(
                name: "license_types");
        }
    }
}
