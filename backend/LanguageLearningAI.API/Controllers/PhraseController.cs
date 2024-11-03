using LanguageLearningAI.Core.Services;
using LanguageLearningAI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LanguageLearningAI.API.Controllers
{
    [ApiController]
    [Route("api/phrases")]
    public class PhraseController : ControllerBase
    {
        private readonly IPhraseService _phraseService;

        public PhraseController(IPhraseService phraseService)
        {
            _phraseService = phraseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Phrase>>> GetAllPhrases()
        {
            var phrases = await _phraseService.GetAllPhrasesAsync();
            return Ok(phrases);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Phrase>> GetPhraseById(int id)
        {
            var phrase = await _phraseService.GetPhraseByIdAsync(id);
            if (phrase == null)
                return NotFound();

            return Ok(phrase);
        }

        [HttpGet("{id}/translation")]
        public async Task<ActionResult<string>> GetTranslation(int id)
        {
            var translation = await _phraseService.GetTranslationAsync(id);
            if (translation == null)
                return NotFound();

            return Ok(translation);
        }

        [HttpPost]
        public async Task<ActionResult> AddPhrase([FromBody] Phrase phrase)
        {
            if (phrase == null)
                return BadRequest("Phrase is null.");

            await _phraseService.AddPhraseAsync(phrase);
            return CreatedAtAction(nameof(GetPhraseById), new { id = phrase.Id }, phrase);
        }
    }
}
