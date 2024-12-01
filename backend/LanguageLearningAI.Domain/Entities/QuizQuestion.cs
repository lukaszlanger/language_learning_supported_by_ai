namespace LanguageLearningAI.Domain.Entities
{
    public class QuizQuestion
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public string Question { get; set; }
        public List<string> Answers { get; set; }
        public string CorrectAnswer { get; set; }
        public string? UserAnswer { get; set; } = null;
        public bool? IsCorrect { get; set; } = null;
    }
}
