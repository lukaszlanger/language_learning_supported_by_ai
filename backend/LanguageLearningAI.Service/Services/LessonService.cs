using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Domain.Entities;
using LanguageLearningAI.Domain.Enums;
using LanguageLearningAI.Service.Repositories;

namespace LanguageLearningAI.Service.Services
{
    public class LessonService
    {
        private readonly LessonRepository _lessonRepository;
        private readonly QuizRepository _quizRepository;

        public LessonService(
            LessonRepository lessonRepository,
            QuizRepository quizRepository)
        {
            _lessonRepository = lessonRepository;
            _quizRepository = quizRepository;
        }

        public async Task<IEnumerable<LessonDto>> GetLessonsByUserAsync(string userId)
        {
            var lessons = await _lessonRepository.GetLessonsByUserAsync(userId);
            return lessons.Select(lesson => new LessonDto
            {
                Id = lesson.Id,
                Topic = lesson.Topic,
                DifficultyLevel = (int)lesson.DifficultyLevel,
                LearningLanguage = lesson.LearningLanguage,
                UserId = lesson.UserId
            });
        }

        public async Task<LessonDto> GetLessonByIdAsync(int id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);

            return new LessonDto
            {
                Id = lesson.Id,
                Topic = lesson.Topic,
                DifficultyLevel = (int)lesson.DifficultyLevel,
                LearningLanguage = lesson.LearningLanguage
            };
        }

        public async Task AddLessonAsync(LessonCreateDto createLessonDto)
        {
            var lesson = new Lesson
            {
                Topic = createLessonDto.Topic,
                DifficultyLevel = (DifficultyLevel)createLessonDto.DifficultyLevel,
                LearningLanguage = createLessonDto.LearningLanguage,
                UserId = createLessonDto.UserId
            };
            await _lessonRepository.AddAsync(lesson);
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
