using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacultativeSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FeedbackUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FeedbackGradeEntities_CourseId",
                table: "FeedbackGradeEntities",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackGradeEntities_Courses_CourseId",
                table: "FeedbackGradeEntities",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackGradeEntities_Courses_CourseId",
                table: "FeedbackGradeEntities");

            migrationBuilder.DropIndex(
                name: "IX_FeedbackGradeEntities_CourseId",
                table: "FeedbackGradeEntities");
        }
    }
}
