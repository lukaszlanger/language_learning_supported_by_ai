using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanguageLearningAI.API.Controllers
{
    [Route("api/lessons")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly LessonService _lessonService;

        public LessonController(LessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet("byUser/{id}")]
        public async Task<IActionResult> GetAllLessons(string userId)
        {
            var lessons = await _lessonService.GetLessonsByUserAsync(userId);
            return Ok(lessons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLessonById(int id)
        {
            var lesson = await _lessonService.GetLessonByIdAsync(id);
            if (lesson == null)
                return NotFound();

            return Ok(lesson);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLesson([FromBody] LessonCreateDto createLessonDto)
        {
            await _lessonService.AddLessonAsync(createLessonDto);
            return CreatedAtAction(nameof(GetLessonById), new { id = createLessonDto.Topic }, createLessonDto);
        }
    }
}
