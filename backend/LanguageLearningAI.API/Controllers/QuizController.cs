using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanguageLearningAI.API.Controllers
{
    [ApiController]
    [Route("api/quizzes")]
    public class QuizController : ControllerBase
    {
        private readonly QuizService _quizService;

        public QuizController(QuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet("byLesson/{lessonId}")]
        public async Task<IActionResult> GetAllQuizzesByLesson(int lessonId)
        {
            var quizzes = await _quizService.GetAllQuizzesByLessonAsync(lessonId);
            return Ok(quizzes);
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateQuiz([FromBody] QuizCreateDto quizCreateDto)
        {
            var quiz = await _quizService.GenerateAndSaveQuizAsync(
                quizCreateDto.Topic,
                quizCreateDto.LearningLanguage,
                quizCreateDto.DifficultyLevel,
                quizCreateDto.LessonId,
                quizCreateDto.UserId
            );

            return Ok(quiz);
        }
    }

}
