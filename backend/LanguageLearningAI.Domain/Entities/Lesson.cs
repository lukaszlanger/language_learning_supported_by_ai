using LanguageLearningAI.Domain.Enums;

namespace LanguageLearningAI.Domain.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public string LearningLanguage { get; set; }
        public string UserId { get; set; }

        public User User { get; set; }
        public ICollection<Phrase> Phrases { get; set; }
    }
}
