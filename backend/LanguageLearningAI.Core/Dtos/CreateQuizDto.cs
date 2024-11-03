namespace LanguageLearningAI.Core.Dtos
{
    /// <summary>
    /// Represents the data transfer object for creating a quiz.
    /// </summary>
    public class CreateQuizDto
    {
        /// <summary>
        /// Gets or sets the ID of the phrase associated with the quiz.
        /// </summary>
        /// <example>1</example>
        public int PhraseId { get; set; }

        /// <summary>
        /// Gets or sets the text of the quiz question.
        /// </summary>
        /// <example>What's the translation of 'Chocolate'?</example>
        public string QuestionText { get; set; }

        /// <summary>
        /// Gets or sets the correct answer for the quiz question.
        /// </summary>
        /// <example>Czekolada</example>
        public string CorrectAnswer { get; set; }
    }
}
