using LanguageLearningAI.Domain.Entities;

namespace LanguageLearningAI.Core.Repositories
{
    public interface IQuizRepository
    {
        Task<IEnumerable<Quiz>> GetAllQuizzesAsync();
        Task<Quiz> GetQuizByIdAsync(int id);
        Task<IEnumerable<Quiz>> GetQuizzesByLessonAsync(int lessonId);
        Task AddQuizAsync(Quiz quiz);
        Task UpdateQuizAsync(Quiz quiz);
    }
}
