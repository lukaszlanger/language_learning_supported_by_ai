using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanguageLearningAI.API.Controllers
{
    [ApiController]
    [Route("api/flashcard")]
    public class FlashcardController : ControllerBase
    {
        private readonly FlashcardService _flashcardService;

        public FlashcardController(FlashcardService flashcardService)
        {
            _flashcardService = flashcardService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlashcardDto>>> GetAllFlashcards()
        {
            var phrases = await _flashcardService.GetAllFlashcardsAsync();
            return Ok(phrases);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FlashcardDto>> GetFlashcardById(int id)
        {
            var phrase = await _flashcardService.GetFlashcardByIdAsync(id);

            return Ok(phrase);
        }

        [HttpPost("generateWithAI")]
        public async Task<IActionResult> GenerateFlashcards([FromBody] FlashcardCreateDto flashcardCreateDto)
        {
            var quiz = await _flashcardService.GenerateAndSaveFlashcardsAsync(
                flashcardCreateDto.Topic,
                flashcardCreateDto.LearningLanguage,
                flashcardCreateDto.NativeLanguage,
                flashcardCreateDto.DifficultyLevel,
                flashcardCreateDto.LessonId
            );

            return Ok(quiz);
        }
    }
}
