namespace LanguageLearningAI.Domain.Entities
{
    public class Quiz
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public string UserId { get; set; }
        public ICollection<QuizQuestion> Questions { get; set; }
    }
}
