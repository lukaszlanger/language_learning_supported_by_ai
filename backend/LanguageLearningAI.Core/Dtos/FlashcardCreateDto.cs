namespace LanguageLearningAI.Core.Dtos
{
    /// <summary>
    /// Represents the data transfer object for creating a flashcard.
    /// </summary>
    public class FlashcardCreateDto
    {
        /// <summary>
        /// Gets or sets the term of the flashcard.
        /// </summary>
        /// <example>Apple</example>
        public string Term { get; set; }

        /// <summary>
        /// Gets or sets the details of the term.
        /// </summary>
        /// <example>Fruit</example>
        public string Details { get; set; }

        /// <summary>
        /// Gets or sets the translation of the term.
        /// </summary>
        /// <example>Jabłko</example>
        public string Translation { get; set; }

        /// <summary>
        /// Gets or sets the example usage of the term in a sentence.
        /// </summary>
        /// <example>Apple is a fruit</example>
        public string Usage { get; set; }

        /// <summary>
        /// Gets or sets the lesson associated with the flashcard.
        /// </summary>
        /// <example>2</example>
        public int LessonId { get; set; }
    }
}
