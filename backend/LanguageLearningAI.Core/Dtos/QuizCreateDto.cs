namespace LanguageLearningAI.Core.Dtos
{
    /// <summary>
    /// Represents the data transfer object for creating a quiz.
    /// </summary>
    public class CreateQuizDto
    {
        /// <summary>
        /// Gets or sets the ID of the lesson associated with the quiz.
        /// </summary>
        /// <example>1</example>
        public int LessonId { get; set; }

        /// <summary>
        /// Gets or sets id of the user assigned to quiz.
        /// </summary>
        /// <example>1</example>
        public string UserId { get; set; }
    }
}
