using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Core.Mapping;
using LanguageLearningAI.Domain.Enums;
using LanguageLearningAI.Service.Repositories;

namespace LanguageLearningAI.Service.Services
{
    public class LessonService
    {
        private readonly LessonRepository _lessonRepository;

        public LessonService(
            LessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<IEnumerable<LessonDto>> GetLessonsByUserAsync(string userId)
        {
            var lessons = await _lessonRepository.GetLessonsByUserAsync(userId);
            return lessons.Select(EntityMapper.Map);
        }

        public async Task<LessonDto> GetLessonByIdAsync(int id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);

            return EntityMapper.Map(lesson);
        }

        public async Task AddLessonAsync(LessonCreateDto createLessonDto)
        {
            await _lessonRepository.AddAsync(EntityMapper.Map(createLessonDto));
        }

        public async Task UpdateLessonAsync(int id, LessonCreateDto lessonDto)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Lesson not found");
            lesson.Topic = lessonDto.Topic;
            lesson.DifficultyLevel = (DifficultyLevel)lessonDto.DifficultyLevel;
            lesson.LearningLanguage = lessonDto.LearningLanguage;

            await _lessonRepository.UpdateAsync(lesson);
        }
    }
}
