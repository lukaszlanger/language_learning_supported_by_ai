namespace LanguageLearningAI.Core.Dtos
{
    public class QuizDto
    {
        public int Id { get; set; }
        public int PhraseId { get; set; }
        public string QuestionText { get; set; }
        public string CorrectAnswer { get; set; }
        public string UserAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public int Attempts { get; set; }
    }
}
