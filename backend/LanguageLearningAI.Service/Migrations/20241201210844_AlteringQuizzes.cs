using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanguageLearningAI.Service.Migrations
{
    /// <inheritdoc />
    public partial class AlteringQuizzes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Phrases_PhraseId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "Attempts",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "CorrectAnswer",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "QuestionText",
                table: "Quizzes");

            migrationBuilder.RenameColumn(
                name: "UserAnswer",
                table: "Quizzes",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "PhraseId",
                table: "Quizzes",
                newName: "LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Quizzes_PhraseId",
                table: "Quizzes",
                newName: "IX_Quizzes_LessonId");

            migrationBuilder.CreateTable(
                name: "QuizQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answers = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizQuestions_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_QuizId",
                table: "QuizQuestions",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Lessons_LessonId",
                table: "Quizzes",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Lessons_LessonId",
                table: "Quizzes");

            migrationBuilder.DropTable(
                name: "QuizQuestions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Quizzes",
                newName: "UserAnswer");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "Quizzes",
                newName: "PhraseId");

            migrationBuilder.RenameIndex(
                name: "IX_Quizzes_LessonId",
                table: "Quizzes",
                newName: "IX_Quizzes_PhraseId");

            migrationBuilder.AddColumn<int>(
                name: "Attempts",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CorrectAnswer",
                table: "Quizzes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "Quizzes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "QuestionText",
                table: "Quizzes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Phrases_PhraseId",
                table: "Quizzes",
                column: "PhraseId",
                principalTable: "Phrases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
