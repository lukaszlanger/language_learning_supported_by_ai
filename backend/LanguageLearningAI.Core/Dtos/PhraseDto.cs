namespace LanguageLearningAI.Core.Dtos
{
    /// <summary>
    /// Represents a phrase data transfer object.
    /// </summary>
    public class PhraseDto
    {
        /// <summary>
        /// Gets or sets the ID of the phrase.
        /// </summary>
        public int Id { get; set; }

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
