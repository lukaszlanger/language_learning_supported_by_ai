using LanguageLearningAI.Core.Dtos;

namespace LanguageLearningAI.Core.Services
{
    public interface IPhraseService
    {
        Task<IEnumerable<PhraseDto>> GetAllPhrasesAsync();
        Task<PhraseDto> GetPhraseByIdAsync(int id);
        Task<string> GetTranslationAsync(int id);
        Task AddPhraseAsync(PhraseCreateDto phraseDto);
    }
}
