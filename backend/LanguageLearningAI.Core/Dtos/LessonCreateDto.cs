namespace LanguageLearningAI.Core.Dtos
{
    /// <summary>
    /// Represents the data transfer object for creating a lesson.
    /// </summary>
    public class LessonCreateDto
    {
        /// <summary>
        /// Gets or sets the topic of the lesson.
        /// </summary>
        /// <example>Grocery store</example>
        public string Topic { get; set; }

        /// <summary>
        /// Gets or sets the difficulty level of the lesson.
        /// </summary>
        /// <example>2</example>
        public int DifficultyLevel { get; set; }

        /// <summary>
        /// Gets or sets the learning language of the lesson.
        /// </summary>
        /// <example>English</example>
        public string LearningLanguage { get; set; }

        /// <summary>
        /// Gets or sets id of the user assigned to lesson.
        /// </summary>
        /// <example>1</example>
        public string UserId { get; set; }
    }
}
