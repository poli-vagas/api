using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace core.src.Infrastructure.Persistence.Sql.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Filter_CompanyId = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    Filter_Type = table.Column<int[]>(type: "integer[]", nullable: false),
                    Filter_CourseId = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    Filter_MinLimitDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Filter_MaxLimitDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Filter_Area = table.Column<List<string>>(type: "text[]", nullable: false),
                    Filter_Workplace = table.Column<int[]>(type: "integer[]", nullable: false),
                    Filter_MinHoursPerDay = table.Column<int>(type: "integer", nullable: true),
                    Filter_MaxHoursPerDay = table.Column<int>(type: "integer", nullable: true),
                    Filter_MinSalary = table.Column<decimal>(type: "numeric", nullable: true),
                    Filter_HasFoodVoucher = table.Column<bool>(type: "boolean", nullable: true),
                    Filter_HasTransportVoucher = table.Column<bool>(type: "boolean", nullable: true),
                    Filter_HasHealthInsurance = table.Column<bool>(type: "boolean", nullable: true),
                    Filter_HasLifeInsurance = table.Column<bool>(type: "boolean", nullable: true),
                    Filter_EnglishLevel = table.Column<int[]>(type: "integer[]", nullable: false),
                    LastRunTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Semester = table.Column<int>(type: "integer", nullable: true),
                    LimitDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    GraduationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IntegrationAgentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Area = table.Column<string>(type: "text", nullable: true),
                    Workplace = table.Column<int>(type: "integer", nullable: true),
                    HoursPerDay = table.Column<int>(type: "integer", nullable: true),
                    Salary = table.Column<decimal>(type: "numeric", nullable: true),
                    Benefits_HasFoodVoucher = table.Column<bool>(type: "boolean", nullable: true),
                    Benefits_HasTransportVoucher = table.Column<bool>(type: "boolean", nullable: true),
                    Benefits_HasHealthInsurance = table.Column<bool>(type: "boolean", nullable: true),
                    Benefits_HasLifeInsurance = table.Column<bool>(type: "boolean", nullable: true),
                    Benefits_Others = table.Column<string>(type: "text", nullable: true),
                    Requirements_EnglishLevel = table.Column<int>(type: "integer", nullable: true),
                    Requirements_OtherLanguages = table.Column<string>(type: "text", nullable: true),
                    Requirements_SoftSkills = table.Column<string>(type: "text", nullable: true),
                    Requirements_HardSkills = table.Column<string>(type: "text", nullable: true),
                    Contact_LinkedinUrl = table.Column<string>(type: "text", nullable: true),
                    Contact_Email = table.Column<string>(type: "text", nullable: true),
                    Contact_EmailInstructions = table.Column<string>(type: "text", nullable: true),
                    Contact_Phone = table.Column<string>(type: "text", nullable: true),
                    Contact_Url = table.Column<string>(type: "text", nullable: true),
                    Contact_ExternalId = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Agents_IntegrationAgentId",
                        column: x => x.IntegrationAgentId,
                        principalTable: "Agents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Jobs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseJob",
                columns: table => new
                {
                    CoursesId = table.Column<Guid>(type: "uuid", nullable: false),
                    JobId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseJob", x => new { x.CoursesId, x.JobId });
                    table.ForeignKey(
                        name: "FK_CourseJob_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseJob_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseJob_JobId",
                table: "CourseJob",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CompanyId",
                table: "Jobs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_IntegrationAgentId",
                table: "Jobs",
                column: "IntegrationAgentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseJob");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
