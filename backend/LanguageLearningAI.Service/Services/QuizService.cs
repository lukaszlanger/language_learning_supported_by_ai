using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Service.Repositories;
using LanguageLearningAI.Domain.Entities;
using LanguageLearningAI.Core.Mapping;

namespace LanguageLearningAI.Service.Services
{
    public class QuizService
    {
        private readonly QuizRepository _quizRepository;
        private readonly OpenAIService _openAIService;
        private readonly LessonService _lessonService;

        public QuizService(
            QuizRepository quizRepository,
            OpenAIService openAIService,
            LessonService lessonService)
        {
            _quizRepository = quizRepository;
            _openAIService = openAIService;
            _lessonService = lessonService;
        }

        public async Task<QuizDto> GenerateAndSaveQuizAsync(int lessonId)
        {
            var lesson = await _lessonService.GetLessonByIdAsync(lessonId) ?? throw new ArgumentException($"Lesson with ID {lessonId} not found.");

            var quizQuestionsDto = await _openAIService.GenerateQuizQuestionsAsync(lesson.Topic, lesson.LearningLanguage, lesson.DifficultyLevel);

            var quiz = new Quiz
            {
                LessonId = lessonId,
                Questions = quizQuestionsDto.Select(q => new QuizQuestion
                {
                    Question = q.Question,
                    Answers = q.Answers.ToList(),
                    CorrectAnswer = q.CorrectAnswer
                }).ToList()
            };

            await _quizRepository.AddAsync(quiz);

            return EntityMapper.Map(quiz);
        }

        public async Task<IEnumerable<QuizDto>> GetAllQuizzesAsync()
        {
            var quizzes = await _quizRepository.GetAllAsync();
            return quizzes.Select(EntityMapper.Map).ToList();
        }

        public async Task<IEnumerable<QuizDto>> GetAllQuizzesByLessonAsync(int lessonId)
        {
            var quizzes = await _quizRepository.GetAllByLessonIdAsync(lessonId);
            return quizzes.Select(EntityMapper.Map).ToList();
        }

        public async Task<QuizDto> GetQuizByIdAsync(int id)
        {
            var quiz = await _quizRepository.GetByIdAsync(id);

            return quiz == null
                ? throw new KeyNotFoundException("Quiz not found")
                : EntityMapper.Map(quiz);
        }

        public async Task UpdateQuizAsync(QuizDto quizDto)
        {
            var quiz = await _quizRepository.GetByIdAsync(quizDto.Id) ?? throw new KeyNotFoundException("Quiz not found");

            quiz.LessonId = quizDto.LessonId;
            quiz.Questions = quizDto.Questions.Select(q => new QuizQuestion
            {
                Question = q.Question,
                Answers = q.Answers.ToList(),
                CorrectAnswer = q.CorrectAnswer
            }).ToList();

            await _quizRepository.UpdateAsync(quiz);
        }
    }
}
