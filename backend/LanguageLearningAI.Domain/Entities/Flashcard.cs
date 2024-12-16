namespace LanguageLearningAI.Domain.Entities
{
    public class Flashcard
    {
        public int Id { get; set; }
        public string Term { get; set; }
        public string Details { get; set; }
        public string Translation { get; set; }
        public string Usage { get; set; }
        public int LessonId { get; set; }

        public Lesson Lesson { get; set; }
    }
}
