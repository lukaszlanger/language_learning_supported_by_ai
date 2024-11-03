namespace LanguageLearningAI.Core.Dtos
{
    /// <summary>
    /// Represents the data transfer object for creating a phrase.
    /// </summary>
    public class PhraseCreateDto
    {
        /// <summary>
        /// Gets or sets the text of the phrase.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the translation of the phrase.
        /// </summary>
        public string Translation { get; set; }
    }

}
