namespace LanguageLearningAI.Core.Dtos
{
    /// <summary>
    /// Represents a flashcard data transfer object.
    /// </summary>
    public class FlashcardDto
    {
        /// <summary>
        /// Gets or sets the ID of the flashcard.
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the term of the flashcard.
        /// </summary>
        /// <example>Apple</example>
        public string Term { get; set; }

        /// <summary>
        /// Gets or sets the details of the term.
        /// </summary>
        /// <example>Jabłko</example>
        public string Details { get; set; }

        /// <summary>
        /// Gets or sets the translation of the term.
        /// </summary>
        /// <example>Jabłko</example>
        public string Translation { get; set; }

        /// <summary>
        /// Gets or sets the example usage of the term in a sentence.
        /// </summary>
        /// <example>Jabłko</example>
        public string Usage { get; set; }

        /// <summary>
        /// Gets or sets the lesson associated with the flashcard.
        /// </summary>
        /// <example>Jabłko</example>
        public int LessonId { get; set; }
    }
}
