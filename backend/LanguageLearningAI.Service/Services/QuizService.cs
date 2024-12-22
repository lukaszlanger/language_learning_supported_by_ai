using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Service.Repositories;
using LanguageLearningAI.Domain.Entities;

namespace LanguageLearningAI.Service.Services
{
    public class QuizService
    {
        private readonly QuizRepository _quizRepository;
        private readonly LessonRepository _lessonRepository;
        private readonly OpenAIService _openAIService;

        public QuizService(
            QuizRepository quizRepository,
            LessonRepository lessonRepository,
            OpenAIService openAIService)
        {
            _quizRepository = quizRepository;
            _lessonRepository = lessonRepository;
            _openAIService = openAIService;
        }

        public async Task<QuizDto> GenerateAndSaveQuizAsync(string topic, string learningLanguage, int difficultyLevel, int lessonId)
        {
            var quizQuestionsDto = await _openAIService.GenerateQuizQuestionsAsync(topic, learningLanguage, difficultyLevel);

            return new QuizDto
            {
                LessonId = lessonId,
                Questions = quizQuestionsDto
            };

            // test

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
        }

        public async Task<IEnumerable<QuizDto>> GetAllQuizzesAsync()
        {
            var quizzes = await _quizRepository.GetAllAsync();

            return quizzes.Select(quiz => new QuizDto
            {
                Id = quiz.Id,
                LessonId = quiz.LessonId,
                Questions = quiz.Questions.Select(q => new QuizQuestionDto
                {
                    Id = q.Id,
                    QuizId = q.QuizId,
                    Question = q.Question,
                    Answers = q.Answers,
                    CorrectAnswer = q.CorrectAnswer,
                    UserAnswer = q.UserAnswer,
                    IsCorrect = q.IsCorrect
                }).ToList()
            });
        }

        public async Task<IEnumerable<QuizDto>> GetAllQuizzesByLessonAsync(int lessonId)
        {
            var quizzes = await _quizRepository.GetAllByLessonIdAsync(lessonId);

            return quizzes.Select(quiz => new QuizDto
            {
                Id = quiz.Id,
                LessonId = quiz.LessonId,
                Questions = quiz.Questions.Select(q => new QuizQuestionDto
                {
                    Id = q.Id,
                    QuizId = q.QuizId,
                    Question = q.Question,
                    Answers = q.Answers,
                    CorrectAnswer = q.CorrectAnswer,
                    UserAnswer = q.UserAnswer,
                    IsCorrect = q.IsCorrect
                }).ToList()
            });
        }


        public async Task<QuizDto> GetQuizByIdAsync(int id)
        {
            var quiz = await _quizRepository.GetByIdAsync(id);

            if (quiz == null)
                throw new KeyNotFoundException("Quiz not found");

            return new QuizDto
            {
                Id = quiz.Id,
                LessonId = quiz.LessonId,
                Questions = quiz.Questions.Select(q => new QuizQuestionDto
                {
                    Id = q.Id,
                    QuizId = q.QuizId,
                    Question = q.Question,
                    Answers = q.Answers,
                    CorrectAnswer = q.CorrectAnswer,
                    UserAnswer = q.UserAnswer,
                    IsCorrect = q.IsCorrect
                }).ToList()
            };
        }

        public async Task AddQuizAsync(QuizCreateDto quizCreateDto)
        {
            var lesson = await _lessonRepository.GetByIdAsync(quizCreateDto.LessonId);
            if (lesson == null)
                throw new KeyNotFoundException("Lesson not found");

            var quiz = new Quiz
            {
                LessonId = quizCreateDto.LessonId,
            };

            await _quizRepository.AddAsync(quiz);
        }

        public async Task UpdateQuizAsync(int id, QuizDto quizDto)
        {
            var quiz = await _quizRepository.GetByIdAsync(id);
            if (quiz == null)
                throw new KeyNotFoundException("Quiz not found");

            quiz.Questions = quizDto.Questions.Select(q => new QuizQuestion
            {
                Id = q.Id,
                QuizId = id,
                Question = q.Question,
                Answers = q.Answers.ToList(),
                CorrectAnswer = q.CorrectAnswer,
                UserAnswer = q.UserAnswer,
                IsCorrect = q.IsCorrect
            }).ToList();

            await _quizRepository.UpdateAsync(quiz);
        }
    }
}
