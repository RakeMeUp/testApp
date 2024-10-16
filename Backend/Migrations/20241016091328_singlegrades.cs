using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class singlegrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuestionGrades_QuestionId",
                table: "QuestionGrades");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionGrades_QuestionId",
                table: "QuestionGrades",
                column: "QuestionId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuestionGrades_QuestionId",
                table: "QuestionGrades");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionGrades_QuestionId",
                table: "QuestionGrades",
                column: "QuestionId");
        }
    }
}
