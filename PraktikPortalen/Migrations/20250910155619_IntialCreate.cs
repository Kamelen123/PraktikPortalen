using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PraktikPortalen.Migrations
{
    /// <inheritdoc />
    public partial class IntialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    City = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Internships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    LocationType = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ApplicationDeadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Internships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Internships_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Internships_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InternshipApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternshipId = table.Column<int>(type: "int", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CoverLetter = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    CvUrl = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternshipApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternshipApplications_Internships_InternshipId",
                        column: x => x.InternshipId,
                        principalTable: "Internships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternshipApplications_Users_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, true, "IT" },
                    { 2, true, "Energy" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "City", "IsActive", "Name", "Website" },
                values: new object[,]
                {
                    { 1, "Göteborg", true, "TechCorp", "https://techcorp.com" },
                    { 2, "Stockholm", true, "GreenEnergy", "https://greenenergy.com" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 10, 12, 0, 0, 0, DateTimeKind.Utc), "admin@test.com", "Admin User", true, "AQAAAAIAAYagAAAAEHfdPaPu3AoXt73wEtI9kk74dORAiPsgVJVbKJDfU6UNi2wjuO11LCYGHrCwUxlthQ==", 2 },
                    { 2, new DateTime(2025, 9, 10, 12, 0, 0, 0, DateTimeKind.Utc), "member1@test.com", "Member One", true, "AQAAAAIAAYagAAAAEJHOQD8WJ9FhT7jFt5WPjdw+iA6FmLgQSsWA+9ranctpdC3Xy2v4vtign4B+sADe+g==", 1 },
                    { 3, new DateTime(2025, 9, 10, 12, 0, 0, 0, DateTimeKind.Utc), "member2@test.com", "Member Two", true, "AQAAAAIAAYagAAAAEJHOQD8WJ9FhT7jFt5WPjdw+iA6FmLgQSsWA+9ranctpdC3Xy2v4vtign4B+sADe+g==", 1 }
                });

            migrationBuilder.InsertData(
                table: "Internships",
                columns: new[] { "Id", "ApplicationDeadline", "CategoryId", "City", "CompanyId", "Description", "IsOpen", "LocationType", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 10, 12, 0, 0, 0, DateTimeKind.Utc), 1, "Göteborg", 1, "Work on APIs", true, 2, "Backend Developer Intern" },
                    { 2, new DateTime(2025, 9, 25, 12, 0, 0, 0, DateTimeKind.Utc), 2, "Stockholm", 2, "Research renewable energy", true, 1, "Energy Analyst Intern" }
                });

            migrationBuilder.InsertData(
                table: "InternshipApplications",
                columns: new[] { "Id", "ApplicantId", "CoverLetter", "CvUrl", "InternshipId", "Status", "SubmittedAt" },
                values: new object[,]
                {
                    { 1, 2, "Excited to join", "cv_member1.pdf", 1, 1, new DateTime(2025, 9, 10, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 3, "Passionate about energy", "cv_member2.pdf", 2, 1, new DateTime(2025, 9, 10, 12, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Name",
                table: "Companies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InternshipApplications_ApplicantId",
                table: "InternshipApplications",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipApplications_InternshipId_ApplicantId",
                table: "InternshipApplications",
                columns: new[] { "InternshipId", "ApplicantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Internships_CategoryId",
                table: "Internships",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Internships_CompanyId",
                table: "Internships",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Internships_IsOpen_ApplicationDeadline",
                table: "Internships",
                columns: new[] { "IsOpen", "ApplicationDeadline" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternshipApplications");

            migrationBuilder.DropTable(
                name: "Internships");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
