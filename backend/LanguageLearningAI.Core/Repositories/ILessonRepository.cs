using LanguageLearningAI.Domain.Entities;

namespace LanguageLearningAI.Core.Repositories
{
    public interface ILessonRepository
    {
        Task<IEnumerable<Lesson>> GetAllAsync();
        Task<IEnumerable<Lesson>> GetLessonsByUserAsync(string userId);
        Task<Lesson> GetByIdAsync(int id);
        Task AddAsync(Lesson lesson);
        Task UpdateAsync(Lesson lesson);
    }
}
