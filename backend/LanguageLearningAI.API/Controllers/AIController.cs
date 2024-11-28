using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Core.Mapping;
using LanguageLearningAI.Core.Services;
using LanguageLearningAI.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanguageLearningAI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIController : ControllerBase
    {
        private readonly IAIService _aiService;

        public AIController(IAIService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost("test")]
        public async Task<string> GenerateTextWithRetryAsync(string prompt, int retryCount = 3)
        {
            for (int i = 0; i < retryCount; i++)
            {
                try
                {
                    if (!System.Text.Encoding.UTF8.GetBytes(prompt).All(b => b < 128))
                    {
                        throw new Exception("Prompt contains non-ASCII characters.");
                    }

                    string wygenerowanyTekst = await _aiService.GenerateTextAsync(prompt);
                    Console.WriteLine(wygenerowanyTekst);
                    return await _aiService.GenerateTextAsync(prompt);
                }
                catch (Exception ex) when (ex.Message.Contains("Model is currently loading"))
                {
                    Console.WriteLine($"Model wciąż się ładuje. Próba {i + 1} z {retryCount}. Czekam 2 minuty...");
                    await Task.Delay(TimeSpan.FromMinutes(1)); // Czekaj 2 minuty przed kolejną próbą
                }
            }
            throw new Exception("Model nie jest dostępny po wielokrotnych próbach.");
        }

        [HttpPost("generate-lesson")]
        public async Task<IActionResult> GenerateLesson([FromBody] CreateLessonDto createLessonDto)
        {
            var lesson = await _aiService.GenerateLessonAsync(createLessonDto.Topic, createLessonDto.LearningLanguage, createLessonDto.DifficultyLevel);
            var lessonDto = EntityMapper.Map(lesson);
            return Ok(lessonDto);
        }

        [HttpPost("generate-quiz/{lessonId}")]
        public async Task<IActionResult> GenerateQuiz(int lessonId, [FromQuery] int difficultyLevel)
        {
            var quizzes = await _aiService.GenerateQuizAsync(lessonId, difficultyLevel);
            var quizDtos = quizzes.Select(q => EntityMapper.Map(q)).ToList();
            return Ok(quizDtos);
        }
    }
}