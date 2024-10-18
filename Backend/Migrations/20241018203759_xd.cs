using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class xd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "QuestionGrades",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionGrades_UserId",
                table: "QuestionGrades",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionGrades_AspNetUsers_UserId",
                table: "QuestionGrades",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionGrades_AspNetUsers_UserId",
                table: "QuestionGrades");

            migrationBuilder.DropIndex(
                name: "IX_QuestionGrades_UserId",
                table: "QuestionGrades");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "QuestionGrades");
        }
    }
}
