using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class xd4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionGrades_Questions_QuestionId1",
                table: "QuestionGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionGrades_UserTestResults_UserTestResultResultId",
                table: "QuestionGrades");

            migrationBuilder.DropIndex(
                name: "IX_QuestionGrades_QuestionId1",
                table: "QuestionGrades");

            migrationBuilder.DropIndex(
                name: "IX_QuestionGrades_UserTestResultResultId",
                table: "QuestionGrades");

            migrationBuilder.DropColumn(
                name: "QuestionId1",
                table: "QuestionGrades");

            migrationBuilder.DropColumn(
                name: "UserTestResultResultId",
                table: "QuestionGrades");

            migrationBuilder.AlterColumn<long>(
                name: "QuestionId",
                table: "QuestionGrades",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<long>(
                name: "ResultId",
                table: "QuestionGrades",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "QuestionGrades",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER")
                .OldAnnotation("Relational:ColumnOrder", 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "QuestionId",
                table: "QuestionGrades",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<long>(
                name: "ResultId",
                table: "QuestionGrades",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "QuestionGrades",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<long>(
                name: "QuestionId1",
                table: "QuestionGrades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserTestResultResultId",
                table: "QuestionGrades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionGrades_QuestionId1",
                table: "QuestionGrades",
                column: "QuestionId1");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionGrades_UserTestResultResultId",
                table: "QuestionGrades",
                column: "UserTestResultResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionGrades_Questions_QuestionId1",
                table: "QuestionGrades",
                column: "QuestionId1",
                principalTable: "Questions",
                principalColumn: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionGrades_UserTestResults_UserTestResultResultId",
                table: "QuestionGrades",
                column: "UserTestResultResultId",
                principalTable: "UserTestResults",
                principalColumn: "ResultId");
        }
    }
}
