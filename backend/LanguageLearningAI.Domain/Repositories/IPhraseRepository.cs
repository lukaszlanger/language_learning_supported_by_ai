using LanguageLearningAI.Domain.Entities;

namespace LanguageLearningAI.Domain.Repositories
{
    public interface IPhraseRepository
    {
        Task<IEnumerable<Phrase>> GetAllAsync();
        Task<Phrase> GetByIdAsync(int id);
        Task AddAsync(Phrase phrase);
    }
}
