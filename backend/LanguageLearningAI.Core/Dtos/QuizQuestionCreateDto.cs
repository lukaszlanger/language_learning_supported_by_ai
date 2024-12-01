namespace LanguageLearningAI.Core.Dtos
{
    /// <summary>
    /// Represents a quiz data transfer object.
    /// </summary>
    public class QuizQuestionCreateDto
    {
        /// <summary>
        /// Gets or sets the ID of the quiz associated with the question.
        /// </summary>
        /// <example>1</example>
        public int QuizId { get; set; }

        /// <summary>
        /// Gets or sets the question text.
        /// </summary>
        /// <example>What is the capital of France?</example>
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets the list of possible answers.
        /// </summary>
        /// <example>["Paris", "London", "Berlin"]</example>
        public IEnumerable<string> Answers { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the correct answer.
        /// </summary>
        /// <example>Paris</example>
        public string CorrectAnswer { get; set; }

        /// <summary>
        /// Gets or sets the user's answer.
        /// </summary>
        /// <example>London</example>
        public string? UserAnswer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user's answer is correct.
        /// </summary>
        public bool? IsCorrect { get; set; }
    }
}
