namespace LanguageLearningAI.Domain.Entities
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public string UserAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public int Attempts { get; set; }
    }
}
