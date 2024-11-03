using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Core.Services;
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
        public async Task<ActionResult<IEnumerable<PhraseDto>>> GetAllPhrases()
        {
            var phrases = await _phraseService.GetAllPhrasesAsync();
            return Ok(phrases);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhraseDto>> GetPhraseById(int id)
        {
            var phrase = await _phraseService.GetPhraseByIdAsync(id);
            if (phrase == null)
                return NotFound();

            return Ok(phrase);
        }

        [HttpPost]
        public async Task<ActionResult> AddPhrase([FromBody] CreatePhraseDto createPhraseDto)
        {
            if (createPhraseDto == null)
                return BadRequest("Phrase data is null.");

            await _phraseService.AddPhraseAsync(createPhraseDto);
            return CreatedAtAction(nameof(GetPhraseById), new { id = createPhraseDto.Text }, createPhraseDto);
        }
    }
}
