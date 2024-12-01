using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Domain.Entities;
using LanguageLearningAI.Service.Repositories;

namespace LanguageLearningAI.Service.Services
{
    public class PhraseService
    {
        private readonly PhraseRepository _phraseRepository;

        public PhraseService(PhraseRepository phraseRepository)
        {
            _phraseRepository = phraseRepository;
        }

        public async Task<IEnumerable<PhraseDto>> GetAllPhrasesAsync()
        {
            var phrases = await _phraseRepository.GetAllAsync();
            return phrases.Select(p => new PhraseDto
            {
                Id = p.Id,
                Text = p.Text,
                Translation = p.Translation
            });
        }

        public async Task<PhraseDto> GetPhraseByIdAsync(int id)
        {
            var phrase = await _phraseRepository.GetByIdAsync(id);
            if (phrase == null)
                return null;

            return new PhraseDto
            {
                Id = phrase.Id,
                Text = phrase.Text,
                Translation = phrase.Translation
            };
        }

        public async Task AddPhraseAsync(CreatePhraseDto createPhraseDto)
        {
            var phrase = new Phrase
            {
                Text = createPhraseDto.Text,
                Translation = createPhraseDto.Translation,
                LessonId = createPhraseDto.LessonId
            };
            await _phraseRepository.AddAsync(phrase);
        }
    }
}
