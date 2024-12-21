using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanguageLearningAI.API.Controllers
{
    [Route("api/lesson")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly LessonService _lessonService;

        public LessonController(LessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllLessonsByUser()
        {
            var lessons = await _lessonService.GetLessons();
            return Ok(lessons);
        }

        [HttpGet("allLessonsByUser/{userId}")]
        public async Task<IActionResult> GetAllLessonsByUser(string userId)
        {
            var lessons = await _lessonService.GetLessonsByUserAsync(userId);
            return Ok(lessons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLessonById(int id)
        {
            var lesson = await _lessonService.GetLessonByIdAsync(id);
            return Ok(lesson);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateLesson([FromBody] LessonCreateDto createLessonDto)
        {
            var lesson = await _lessonService.AddLessonAsync(createLessonDto);
            return Ok(lesson);
        }
    }
}
