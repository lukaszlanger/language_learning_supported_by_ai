using LanguageLearningAI.API;
using LanguageLearningAI.Domain.Entities;
using LanguageLearningAI.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LanguageLearningAI.Service.Repositories
{
    public class PhraseRepository : IPhraseRepository
    {
        private readonly ApplicationDbContext _context;

        public PhraseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Phrase>> GetAllAsync()
        {
            return await _context.Phrases.ToListAsync();
        }

        public async Task<Phrase> GetByIdAsync(int id)
        {
            return await _context.Phrases.FindAsync(id);
        }

        public async Task AddAsync(Phrase phrase)
        {
            await _context.Phrases.AddAsync(phrase);
            await _context.SaveChangesAsync();
        }
    }
}
