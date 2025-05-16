using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacultativeSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCourseEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasUnmarkedStudents",
                table: "Courses",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasUnmarkedStudents",
                table: "Courses");
        }
    }
}
