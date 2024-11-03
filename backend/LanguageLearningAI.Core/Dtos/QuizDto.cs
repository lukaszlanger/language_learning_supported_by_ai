namespace LanguageLearningAI.Core.Dtos
{
    /// <summary>
    /// Represents a quiz data transfer object.
    /// </summary>
    public class QuizDto
    {
        /// <summary>
        /// Gets or sets the ID of the quiz.
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

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

        /// <summary>
        /// Gets or sets the user's answer for the quiz question.
        /// </summary>
        /// <example>Czekolada</example>
        public string UserAnswer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user's answer is correct.
        /// </summary>
        /// <example>true</example>
        public bool IsCorrect { get; set; }

        /// <summary>
        /// Gets or sets the number of attempts made for the quiz question.
        /// </summary>
        /// <example>2</example>
        public int Attempts { get; set; }
    }
}
