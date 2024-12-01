namespace LanguageLearningAI.Core.Dtos
{
    /// <summary>
    /// Represents a quiz data transfer object.
    /// </summary>
    public class QuizDto
    {
        /// <summary>
        /// Gets or sets the ID of the quiz.
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the lesson associated with the quiz.
        /// </summary>
        /// <example>1</example>
        public int LessonId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user associated with the quiz.
        /// </summary>
        /// <example>1</example>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the list of quiz questions.
        /// </summary>
        public IEnumerable<QuizQuestionDto> Questions { get; set; } = new List<QuizQuestionDto>();
    }
}
