using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Core.Repositories;
using LanguageLearningAI.Core.Services;
using LanguageLearningAI.Domain.Entities;

namespace LanguageLearningAI.Service.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<IEnumerable<QuizDto>> GetAllQuizzesAsync()
        {
            var quizzes = await _quizRepository.GetAllQuizzesAsync();
            return quizzes.Select(q => new QuizDto
            {
                Id = q.Id,
                PhraseId = q.PhraseId,
                QuestionText = q.QuestionText,
                CorrectAnswer = q.CorrectAnswer,
                UserAnswer = q.UserAnswer,
                IsCorrect = q.IsCorrect,
                Attempts = q.Attempts
            });
        }

        public async Task<QuizDto> GetQuizByIdAsync(int id)
        {
            var quiz = await _quizRepository.GetQuizByIdAsync(id);
            return new QuizDto
            {
                Id = quiz.Id,
                PhraseId = quiz.PhraseId,
                QuestionText = quiz.QuestionText,
                CorrectAnswer = quiz.CorrectAnswer,
                UserAnswer = quiz.UserAnswer,
                IsCorrect = quiz.IsCorrect,
                Attempts = quiz.Attempts
            };
        }

        public async Task<IEnumerable<QuizDto>> GetQuizzesByLessonAsync(int lessonId)
        {
            var quizzes = await _quizRepository.GetQuizzesByLessonAsync(lessonId);
            return quizzes.Select(q => new QuizDto
            {
                Id = q.Id,
                PhraseId = q.PhraseId,
                QuestionText = q.QuestionText,
                CorrectAnswer = q.CorrectAnswer,
                UserAnswer = q.UserAnswer,
                IsCorrect = q.IsCorrect,
                Attempts = q.Attempts
            });
        }

        public async Task CreateQuizAsync(CreateQuizDto createQuizDto)
        {
            var quiz = new Quiz
            {
                PhraseId = createQuizDto.PhraseId,
                QuestionText = createQuizDto.QuestionText,
                CorrectAnswer = createQuizDto.CorrectAnswer,
                UserAnswer = null,
                IsCorrect = false,
                Attempts = 0
            };
            await _quizRepository.AddQuizAsync(quiz);
        }

        public async Task UpdateQuizAsync(QuizDto quizDto)
        {
            var quiz = await _quizRepository.GetQuizByIdAsync(quizDto.Id);
            if (quiz == null)
                throw new KeyNotFoundException();

            quiz.UserAnswer = quizDto.UserAnswer;
            quiz.IsCorrect = quizDto.IsCorrect;
            quiz.Attempts = quizDto.Attempts;

            await _quizRepository.UpdateQuizAsync(quiz);
        }
    }
}
