namespace LanguageLearningAI.Core.Dtos
{
    /// <summary>
    /// Represents the data transfer object for creating a quiz.
    /// </summary>
    public class QuizCreateDto
    {
        /// <summary>
        /// Gets or sets the topic of the lesson.
        /// </summary>
        /// <example>Grocery store</example>
        public string Topic { get; set; }

        /// <summary>
        /// Gets or sets the difficulty level of the lesson.
        /// </summary>
        /// <example>2</example>
        public int DifficultyLevel { get; set; }

        /// <summary>
        /// Gets or sets the learning language of the lesson.
        /// </summary>
        /// <example>en</example>
        public string LearningLanguage { get; set; }

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
