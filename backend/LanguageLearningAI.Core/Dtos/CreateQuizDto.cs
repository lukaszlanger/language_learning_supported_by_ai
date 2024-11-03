namespace LanguageLearningAI.Core.Dtos
{
    /// <summary>
    /// Represents the data transfer object for creating a quiz.
    /// </summary>
    public class CreateQuizDto
    {
        /// <summary>
        /// Gets or sets the phrase ID.
        /// </summary>
        public int PhraseId { get; set; }

        /// <summary>
        /// Gets or sets the question text.
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// Gets or sets the correct answer.
        /// </summary>
        public string CorrectAnswer { get; set; }
    }
}
