using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace core.src.Infrastructure.Persistence.Sql.Migrations
{
    /// <inheritdoc />
    public partial class AddWeekNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WeekNumber",
                table: "Jobs",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeekNumber",
                table: "Jobs");
        }
    }
}
