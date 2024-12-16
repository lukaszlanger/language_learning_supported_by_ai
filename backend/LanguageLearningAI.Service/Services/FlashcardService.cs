using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Core.Mapping;
using LanguageLearningAI.Service.Repositories;

namespace LanguageLearningAI.Service.Services
{
    public class FlashcardService
    {
        private readonly FlashcardRepository _flashcardRepository;

        public FlashcardService(FlashcardRepository flashcardRepository)
        {
            _flashcardRepository = flashcardRepository;
        }

        public async Task<IEnumerable<FlashcardDto>> GetAllPhrasesAsync()
        {
            var phrases = await _flashcardRepository.GetAllAsync();
            return phrases.Select(EntityMapper.Map);
        }

        public async Task<FlashcardDto> GetPhraseByIdAsync(int id)
        {
            var flashcard = await _flashcardRepository.GetByIdAsync(id);

            return EntityMapper.Map(flashcard);
        }

        public async Task<FlashcardDto> AddPhraseAsync(FlashcardCreateDto flashcardCreateDto)
        {
            var flashcard = await _flashcardRepository.AddAsync(EntityMapper.Map(flashcardCreateDto));
            return await GetPhraseByIdAsync(flashcard);
        }
    }
}
