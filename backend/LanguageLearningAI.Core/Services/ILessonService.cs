using LanguageLearningAI.Core.Dtos;

namespace LanguageLearningAI.Core.Services
{
    public interface ILessonService
    {
        Task<IEnumerable<LessonDto>> GetAllLessonsAsync();
        Task<IEnumerable<LessonDto>> GetLessonsByUserAsync(string userId);
        Task<LessonDto> GetLessonByIdAsync(int id);
        Task AddLessonAsync(CreateLessonDto createLessonDto);
        Task UpdateLessonAsync(int id, LessonDto lessonDto);
    }
}
