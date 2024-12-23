using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanguageLearningAI.API.Controllers
{
    [ApiController]
    [Route("api/quiz")]
    public class QuizController : ControllerBase
    {
        private readonly QuizService _quizService;

        public QuizController(QuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpPost("generateWithAI")]
        public async Task<IActionResult> GenerateQuiz([FromQuery] int lessonId)
        {
            var quiz = await _quizService.GenerateAndSaveQuizAsync(lessonId);

            return Ok(quiz);
        }

        [HttpGet("allByLessonId/{lessonId}")]
        public async Task<ActionResult<IEnumerable<QuizDto>>> GetAllQuizzesByLesson(int lessonId)
        {
            var quizzes = await _quizService.GetAllQuizzesByLessonAsync(lessonId);
            return Ok(quizzes);
        }
    }

}
