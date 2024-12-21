using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlashcardDto>>> GetAllFlashcards()
        {
            var phrases = await _flashcardService.GetAllFlashcardsAsync();
            return Ok(phrases);
        }

        [HttpGet("getAllByLessonId/{lessonId}")]
        public async Task<ActionResult<IEnumerable<FlashcardDto>>> GetFlashcardsByLessonId(int lessonId)
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
                lessonDetails.LearningLanguage,
                userDetails.NativeLanguage,
                lessonDetails.Topic,
                lessonDetails.DifficultyLevel,
                flashcardCreateDto.Term,
                lessonDetails.Id));
        }

        [HttpPost("generateWithAI")]
        public async Task<IActionResult> GenerateFlashcardsWithAI([FromBody] FlashcardGenerateWithAIDto flashcardGenerateWithAiDto)
        {
            return Ok(await _flashcardService.GenerateAndSaveFlashcardsAsync(flashcardGenerateWithAiDto));
        }
    }
}
