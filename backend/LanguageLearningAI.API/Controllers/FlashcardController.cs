using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanguageLearningAI.API.Controllers
{
    [ApiController]
    [Route("api/flashcards")]
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
            var phrases = await _flashcardService.GetAllPhrasesAsync();
            return Ok(phrases);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FlashcardDto>> GetFlashcardById(int id)
        {
            var phrase = await _flashcardService.GetPhraseByIdAsync(id);

            return Ok(phrase);
        }

        [HttpPost]
        public async Task<ActionResult> AddPhrase([FromBody] FlashcardCreateDto flashcardCreateDto)
        {
            var flashcardId = await _flashcardService.AddPhraseAsync(flashcardCreateDto);
            return CreatedAtAction(nameof(GetFlashcardById), new { id = flashcardId }, flashcardCreateDto);
        }
    }
}
