using LanguageLearningAI.Core.Repositories;
using LanguageLearningAI.Core.Services;

namespace LanguageLearningAI.Service.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }
    }
}
