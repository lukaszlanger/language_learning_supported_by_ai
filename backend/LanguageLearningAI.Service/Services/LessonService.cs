using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Core.Mapping;
using LanguageLearningAI.Domain.Enums;
using LanguageLearningAI.Service.Repositories;

namespace LanguageLearningAI.Service.Services
{
    public class LessonService
    {
        private readonly LessonRepository _lessonRepository;
        private readonly FlashcardRepository _flashcardRepository;
        private readonly QuizRepository _quizRepository;

        public LessonService(
            LessonRepository lessonRepository,
            FlashcardRepository flashcardRepository,
            QuizRepository quizRepository)
        {
            _lessonRepository = lessonRepository;
            _flashcardRepository = flashcardRepository;
            _quizRepository = quizRepository;
        }

        public async Task<IEnumerable<LessonDto>> GetLessons()
        {
            var lessons = await _lessonRepository.GetLessons();

            return lessons.Select(lesson => EntityMapper.Map(lesson)).ToList();
        }

        public async Task<IEnumerable<LessonDto>> GetLessonsByUserAsync(string userId)
        {
            var lessons = await _lessonRepository.GetLessonsByUserAsync(userId);

            var lessonDtos = new List<LessonDto>();

            foreach (var lesson in lessons)
            {
                var flashcardsCount = await _flashcardRepository.GetFlashcardCountByLessonIdAsync(lesson.Id);
                var quizzesCount = await _quizRepository.GetQuizCountByLessonIdAsync(lesson.Id);

                var lessonDto = EntityMapper.Map(lesson);
                lessonDto.FlashcardsCount = flashcardsCount;
                lessonDto.QuizzesCount = quizzesCount;

                lessonDtos.Add(lessonDto);
            }

            return lessonDtos;
        }

        public async Task<LessonDto> GetLessonByIdAsync(int id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            return EntityMapper.Map(lesson);
        }

        public async Task<LessonDto> CreateAsync(LessonCreateDto createLessonDto)
        {
            var lessonId = await _lessonRepository.AddAsync(EntityMapper.Map(createLessonDto));
            return await GetLessonByIdAsync(lessonId);
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
