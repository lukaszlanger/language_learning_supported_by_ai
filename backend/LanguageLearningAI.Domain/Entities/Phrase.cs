namespace LanguageLearningAI.Domain.Entities
{
    public class Phrase
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Translation { get; set; }
        public int LessonId { get; set; }

        public Lesson Lesson { get; set; }
    }
}
