using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MobileDataTerminal.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EyeColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EyeColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LicenseStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LicenseTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sexes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sexes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Licenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    IssuanceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SexId = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NationalityId = table.Column<int>(type: "int", nullable: false),
                    EyeColorId = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    BloodTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licenses", x => x.Id);
                    table.ForeignKey(
                        name: "fk_licenses_blood_types",
                        column: x => x.BloodTypeId,
                        principalTable: "BloodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_licenses_eye_colors",
                        column: x => x.EyeColorId,
                        principalTable: "EyeColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_licenses_nationalities",
                        column: x => x.NationalityId,
                        principalTable: "Nationalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_licenses_sexes",
                        column: x => x.SexId,
                        principalTable: "Sexes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_licenses_statuses",
                        column: x => x.StatusId,
                        principalTable: "LicenseStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_licenses_types",
                        column: x => x.TypeId,
                        principalTable: "LicenseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "fk_users_licenses",
                        column: x => x.LicenseId,
                        principalTable: "Licenses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_users_user_roles",
                        column: x => x.RoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BloodTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "A+" },
                    { 2, "A-" },
                    { 3, "B+" },
                    { 4, "B-" },
                    { 5, "AB+" },
                    { 6, "AB-" },
                    { 7, "O+" },
                    { 8, "O-" }
                });

            migrationBuilder.InsertData(
                table: "EyeColors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Brown" },
                    { 2, "Blue" },
                    { 3, "Green" },
                    { 4, "Hazel" },
                    { 5, "Gray" },
                    { 6, "Amber" }
                });

            migrationBuilder.InsertData(
                table: "LicenseStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Revoked" },
                    { 3, "Expired" }
                });

            migrationBuilder.InsertData(
                table: "LicenseTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Professional" },
                    { 2, "NonProfessional" },
                    { 3, "StudentPermit" }
                });

            migrationBuilder.InsertData(
                table: "Nationalities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Filipino" },
                    { 2, "American" },
                    { 3, "Japanese" },
                    { 4, "Chinese" },
                    { 5, "SouthKorean" },
                    { 6, "Taiwanese" },
                    { 7, "Singaporean" },
                    { 8, "Malaysian" },
                    { 9, "Indonesian" },
                    { 10, "Thai" },
                    { 11, "Cambodian" },
                    { 12, "Vietnamese" },
                    { 13, "British" },
                    { 14, "Australian" },
                    { 15, "Canadian" },
                    { 16, "NewZealander" },
                    { 17, "Spanish" },
                    { 18, "Portuguese" },
                    { 19, "Mexican" },
                    { 20, "German" },
                    { 21, "Dutch" },
                    { 22, "French" },
                    { 23, "Italian" },
                    { 24, "Swiss" },
                    { 25, "Swedish" },
                    { 26, "Norwegian" },
                    { 27, "Ukrainian" },
                    { 28, "Russian" },
                    { 29, "SouthAfrican" },
                    { 30, "Indian" }
                });

            migrationBuilder.InsertData(
                table: "Sexes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Male" },
                    { 2, "Female" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Civilian" },
                    { 2, "Officer" },
                    { 3, "Supervisor" },
                    { 4, "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "uq_blood_types_name",
                table: "BloodTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_eye_colors_name",
                table: "EyeColors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Licenses_BloodTypeId",
                table: "Licenses",
                column: "BloodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Licenses_EyeColorId",
                table: "Licenses",
                column: "EyeColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Licenses_NationalityId",
                table: "Licenses",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Licenses_SexId",
                table: "Licenses",
                column: "SexId");

            migrationBuilder.CreateIndex(
                name: "IX_Licenses_StatusId",
                table: "Licenses",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Licenses_TypeId",
                table: "Licenses",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "uq_licenses_number",
                table: "Licenses",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_license_statuses_name",
                table: "LicenseStatuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_license_types_name",
                table: "LicenseTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_nationalities_name",
                table: "Nationalities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_sexes_name",
                table: "Sexes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_user_roles_name",
                table: "UserRoles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "uq_users_email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_users_license_id",
                table: "Users",
                column: "LicenseId",
                unique: true,
                filter: "[LicenseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "uq_users_username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Licenses");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "BloodTypes");

            migrationBuilder.DropTable(
                name: "EyeColors");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropTable(
                name: "Sexes");

            migrationBuilder.DropTable(
                name: "LicenseStatuses");

            migrationBuilder.DropTable(
                name: "LicenseTypes");
        }
    }
}
