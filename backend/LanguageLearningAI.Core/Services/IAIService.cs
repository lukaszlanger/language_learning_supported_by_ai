using LanguageLearningAI.Domain.Entities;

namespace LanguageLearningAI.Core.Services
{
    public interface IAIService
    {
        public Task<string> GenerateTextAsync(string prompt);
        Task<Lesson> GenerateLessonAsync(string topic, string language, int difficultyLevel);
        Task<IEnumerable<Quiz>> GenerateQuizAsync(int lessonId, int difficultyLevel);
    }
}