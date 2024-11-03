using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Core.Repositories;
using LanguageLearningAI.Core.Services;
using LanguageLearningAI.Domain.Entities;
using LanguageLearningAI.Domain.Enums;

namespace LanguageLearningAI.Service.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;

        public LessonService(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<IEnumerable<LessonDto>> GetAllLessonsAsync()
        {
            var lessons = await _lessonRepository.GetAllAsync();
            return lessons.Select(lesson => new LessonDto
            {
                Id = lesson.Id,
                Title = lesson.Title,
                DifficultyLevel = (int)lesson.DifficultyLevel,
                LearningLanguage = lesson.LearningLanguage
            });
        }

        public async Task<IEnumerable<LessonDto>> GetLessonsByUserAsync(string userId)
        {
            var lessons = await _lessonRepository.GetLessonsByUserAsync(userId);
            return lessons.Select(lesson => new LessonDto
            {
                Id = lesson.Id,
                Title = lesson.Title,
                DifficultyLevel = (int)lesson.DifficultyLevel,
                LearningLanguage = lesson.LearningLanguage,
                UserId = lesson.UserId
            });
        }

        public async Task<LessonDto> GetLessonByIdAsync(int id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            if (lesson == null)
                return null;

            return new LessonDto
            {
                Id = lesson.Id,
                Title = lesson.Title,
                DifficultyLevel = (int)lesson.DifficultyLevel,
                LearningLanguage = lesson.LearningLanguage
            };
        }

        public async Task AddLessonAsync(CreateLessonDto createLessonDto)
        {
            var lesson = new Lesson
            {
                Title = createLessonDto.Title,
                DifficultyLevel = (DifficultyLevel)createLessonDto.DifficultyLevel,
                LearningLanguage = createLessonDto.LearningLanguage
            };
            await _lessonRepository.AddAsync(lesson);
        }

        public async Task UpdateLessonAsync(int id, LessonDto lessonDto)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            if (lesson == null)
                throw new KeyNotFoundException("Lesson not found");

            lesson.Title = lessonDto.Title;
            lesson.DifficultyLevel = (DifficultyLevel)lessonDto.DifficultyLevel;
            lesson.LearningLanguage = lessonDto.LearningLanguage;

            await _lessonRepository.UpdateAsync(lesson);
        }
    }
}
