using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanguageLearningAI.Service.Migrations
{
    /// <inheritdoc />
    public partial class RenameTitleToTopicInLesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Lessons",
                newName: "Topic");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Topic",
                table: "Lessons",
                newName: "Title");
        }
    }
}
