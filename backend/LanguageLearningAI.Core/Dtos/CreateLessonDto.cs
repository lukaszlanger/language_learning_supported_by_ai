namespace LanguageLearningAI.Core.Dtos
{
    /// <summary>
    /// Represents the data transfer object for creating a lesson.
    /// </summary>
    public class CreateLessonDto
    {
        /// <summary>
        /// Gets or sets the title of the lesson.
        /// </summary>
        /// <example>Grocery store</example>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the difficulty level of the lesson.
        /// </summary>
        /// <example>2</example>
        public int DifficultyLevel { get; set; }

        /// <summary>
        /// Gets or sets the learning language of the lesson.
        /// </summary>
        /// <example>en</example>
        public string LearningLanguage { get; set; }
    }
}
