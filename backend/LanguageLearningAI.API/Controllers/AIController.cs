using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Core.Mapping;
using LanguageLearningAI.Core.Services;
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