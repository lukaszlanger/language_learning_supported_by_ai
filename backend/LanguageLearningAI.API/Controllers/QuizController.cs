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

        /*
        [HttpGet]
        public async Task<IActionResult> GetAllQuizzes()
        {
            var quizzes = await _quizService.GetAllQuizzesAsync();
            return Ok(quizzes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuizById(int id)
        {
            var quiz = await _quizService.GetQuizByIdAsync(id);
            if (quiz == null)
                return NotFound();
            return Ok(quiz);
        }

        [HttpGet("byLesson")]
        public async Task<IActionResult> GetQuizResultsByUserAndPhrase(int lessonId)
        {
            var results = await _quizService.GetQuizzesByLessonAsync(lessonId);
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuiz([FromBody] CreateQuizDto createQuizDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _quizService.CreateQuizAsync(createQuizDto);
            return CreatedAtAction(nameof(GetAllQuizzes), new { });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuiz([FromBody] QuizDto quizDto)
        {
            try
            {
                await _quizService.UpdateQuizAsync(quizDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Quiz not found" });
            }
        }
        */
    }

}
