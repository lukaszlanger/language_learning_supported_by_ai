using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanguageLearningAI.Service.Migrations
{
    /// <inheritdoc />
    public partial class FlashcardsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phrases_Lessons_LessonId",
                table: "Phrases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Phrases",
                table: "Phrases");

            migrationBuilder.RenameTable(
                name: "Phrases",
                newName: "Flashcards");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Flashcards",
                newName: "Usage");

            migrationBuilder.RenameIndex(
                name: "IX_Phrases_LessonId",
                table: "Flashcards",
                newName: "IX_Flashcards_LessonId");

            migrationBuilder.AlterColumn<string>(
                name: "UserAnswer",
                table: "QuizQuestions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Flashcards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Term",
                table: "Flashcards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flashcards",
                table: "Flashcards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flashcards_Lessons_LessonId",
                table: "Flashcards",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flashcards_Lessons_LessonId",
                table: "Flashcards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flashcards",
                table: "Flashcards");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "Flashcards");

            migrationBuilder.DropColumn(
                name: "Term",
                table: "Flashcards");

            migrationBuilder.RenameTable(
                name: "Flashcards",
                newName: "Phrases");

            migrationBuilder.RenameColumn(
                name: "Usage",
                table: "Phrases",
                newName: "Text");

            migrationBuilder.RenameIndex(
                name: "IX_Flashcards_LessonId",
                table: "Phrases",
                newName: "IX_Phrases_LessonId");

            migrationBuilder.AlterColumn<string>(
                name: "UserAnswer",
                table: "QuizQuestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Phrases",
                table: "Phrases",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Phrases_Lessons_LessonId",
                table: "Phrases",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
