using LanguageLearningAI.Core.Services;
using LanguageLearningAI.Domain.Entities;
using LanguageLearningAI.Domain.Repositories;

namespace LanguageLearningAI.Service.Services
{
    public class PhraseService : IPhraseService
    {
        private readonly IPhraseRepository _phraseRepository;

        public PhraseService(IPhraseRepository phraseRepository)
        {
            _phraseRepository = phraseRepository;
        }

        public async Task<IEnumerable<Phrase>> GetAllPhrasesAsync()
        {
            return await _phraseRepository.GetAllAsync();
        }

        public async Task<Phrase> GetPhraseByIdAsync(int id)
        {
            return await _phraseRepository.GetByIdAsync(id);
        }

        public async Task<string> GetTranslationAsync(int id)
        {
            var phrase = await _phraseRepository.GetByIdAsync(id);
            return phrase?.Translation;
        }

        public async Task AddPhraseAsync(Phrase phrase)
        {
            await _phraseRepository.AddAsync(phrase);
        }
    }
}
