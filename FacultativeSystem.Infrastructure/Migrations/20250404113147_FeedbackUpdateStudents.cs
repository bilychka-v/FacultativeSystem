using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacultativeSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FeedbackUpdateStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackGradeEntities_Students_StudentEntityId",
                table: "FeedbackGradeEntities");

            migrationBuilder.DropIndex(
                name: "IX_FeedbackGradeEntities_StudentEntityId",
                table: "FeedbackGradeEntities");

            migrationBuilder.DropColumn(
                name: "StudentEntityId",
                table: "FeedbackGradeEntities");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackGradeEntities_StudentId",
                table: "FeedbackGradeEntities",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackGradeEntities_Students_StudentId",
                table: "FeedbackGradeEntities",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackGradeEntities_Students_StudentId",
                table: "FeedbackGradeEntities");

            migrationBuilder.DropIndex(
                name: "IX_FeedbackGradeEntities_StudentId",
                table: "FeedbackGradeEntities");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentEntityId",
                table: "FeedbackGradeEntities",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackGradeEntities_StudentEntityId",
                table: "FeedbackGradeEntities",
                column: "StudentEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackGradeEntities_Students_StudentEntityId",
                table: "FeedbackGradeEntities",
                column: "StudentEntityId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
