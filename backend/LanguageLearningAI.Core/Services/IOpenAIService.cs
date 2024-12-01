namespace LanguageLearningAI.Core.Services
{
    public interface IOpenAIService
    {
        public Task<string> GenerateQuizAsync (string topic, string learningLanguage, int difficultyLevel);
    }
}