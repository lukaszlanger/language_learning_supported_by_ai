namespace LanguageLearningAI.Core.Dtos
{
    /// <summary>
    /// Represents a lesson.
    /// </summary>
    public class LessonDto
    {
        /// <summary>
        /// Gets or sets the lesson ID.
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the lesson title.
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
        /// Gets or sets id of the user assigned to lesson.
        /// </summary>
        /// <example>1</example>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets count of flashcards created for the lesson.
        /// </summary>
        /// <example>10</example>
        public int FlashcardsCount { get; set; }

        /// <summary>
        /// Gets or sets count of quizzes associated with the lesson.
        /// </summary>
        /// <example>3</example>
        public int QuizzesCount { get; set; }
    }
}
