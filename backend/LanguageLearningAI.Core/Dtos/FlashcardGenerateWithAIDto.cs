namespace LanguageLearningAI.Core.Dtos
{
    /// <summary>
    /// Represents the data transfer object for creating a flashcard.
    /// </summary>
    public class FlashcardGenerateWithAIDto
    {
        /// <summary>
        /// Gets or sets the id of the user.
        /// </summary>
        /// <example>1</example>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the lesson associated with the flashcard.
        /// </summary>
        /// <example>2</example>
        public int LessonId { get; set; }
    }
}
