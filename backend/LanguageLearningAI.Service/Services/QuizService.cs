using LanguageLearningAI.Service.Repositories;

namespace LanguageLearningAI.Service.Services
{
    public class QuizService
    {
        private readonly QuizRepository _quizRepository;

        public QuizService(QuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }
    }
}
