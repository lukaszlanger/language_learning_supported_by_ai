namespace LanguageLearningAI.Core.Dtos
{
    /// <summary>
    /// Represents the data transfer object for creating a phrase.
    /// </summary>
    public class CreatePhraseDto
    {
        /// <summary>
        /// Gets or sets the text of the phrase.
        /// </summary>
        /// <example>Apple</example>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the translation of the phrase.
        /// </summary>
        /// <example>Jabłko</example>
        public string Translation { get; set; }

        /// <summary>
        /// Gets or sets the lesson ID of the phrase.
        /// </summary>
        /// <example>1</example>
        public int LessonId { get; set; }
    }
}
