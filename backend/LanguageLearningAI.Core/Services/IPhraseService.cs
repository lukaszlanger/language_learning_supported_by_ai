using LanguageLearningAI.Domain.Entities;

namespace LanguageLearningAI.Core.Services
{
    public interface IPhraseService
    {
        Task<IEnumerable<Phrase>> GetAllPhrasesAsync();
        Task<Phrase> GetPhraseByIdAsync(int id);
        Task<string> GetTranslationAsync(int id);
        Task AddPhraseAsync(Phrase phrase);
    }
}
