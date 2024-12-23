using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace LanguageLearningAI.API.Controllers
{
    [ApiController]
    [Route("api/flashcard")]
    public class FlashcardController : ControllerBase
    {
        private readonly FlashcardService _flashcardService;
        private readonly LessonService _lessonService;
        private readonly AuthService _authService;

        public FlashcardController(
            FlashcardService flashcardService,
            LessonService lessonService,
            AuthService authService)
        {
            _flashcardService = flashcardService;
            _lessonService = lessonService;
            _authService = authService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<FlashcardDto>>> GetAllFlashcards()
        {
            var phrases = await _flashcardService.GetAllFlashcardsAsync();
            return Ok(phrases);
        }

        [HttpGet("allByLessonId/{lessonId}")]
        public async Task<ActionResult<IEnumerable<FlashcardDto>>> GetAllFlashcardsByLessonId(int lessonId)
        {
            var flashcards = await _flashcardService.GetFlashcardsByLessonIdAsync(lessonId);

            return Ok(flashcards);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateFlashcard([FromBody] FlashcardCreateDto flashcardCreateDto)
        {
            var lessonDetails = _lessonService.GetLessonByIdAsync(flashcardCreateDto.LessonId).Result;
            var userDetails = _authService.GetUserByIdAsync(lessonDetails.UserId).Result;
            return Ok(await _flashcardService.CreateAsync(
                userDetails.NativeLanguage,
                lessonDetails.LearningLanguage,
                lessonDetails.Topic,
                lessonDetails.DifficultyLevel,
                lessonDetails.Id,
                flashcardCreateDto.Term,
                flashcardCreateDto.Translation,
                flashcardCreateDto.Details,
                flashcardCreateDto.Usage));
        }

        [HttpPost("generateWithAI")]
        public async Task<IActionResult> GenerateFlashcardsWithAI([FromQuery] string userId, [FromQuery] int lessonId)
        {
            return Ok(await _flashcardService.GenerateAndSaveFlashcardsAsync(userId, lessonId));
        }
    }
}
