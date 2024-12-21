namespace LanguageLearningAI.Core.Dtos
{
    /// <summary>
    /// Represents the data transfer object for creating a flashcard.
    /// </summary>
    public class FlashcardGenerateWithAIDto
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
        /// <example>English</example>
        public string LearningLanguage { get; set; }

        /// <summary>
        /// Gets or sets the native language of the user.
        /// </summary>
        /// <example>Polish</example>
        public string NativeLanguage { get; set; }

        /// <summary>
        /// Gets or sets the lesson associated with the flashcard.
        /// </summary>
        /// <example>2</example>
        public int LessonId { get; set; }
    }
}
