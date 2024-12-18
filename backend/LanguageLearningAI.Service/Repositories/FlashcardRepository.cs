using LanguageLearningAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LanguageLearningAI.Service.Repositories
{
    public class FlashcardRepository
    {
        private readonly ApplicationDbContext _context;

        public FlashcardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Flashcard>> GetAllAsync()
        {
            return await _context.Flashcards.ToListAsync();
        }

        public async Task<IEnumerable<Flashcard>> GetAllByLessonIdAsync(int id)
        {
            return await _context.Flashcards.Where(f => f.LessonId == id).ToListAsync();
        }

        public async Task<Flashcard> GetByIdAsync(int id)
        {
            return await _context.Flashcards.FindAsync(id);
        }

        public async Task<int> AddAsync(Flashcard flashcard)
        {
            await _context.Flashcards.AddAsync(flashcard);
            await _context.SaveChangesAsync();

            return flashcard.Id;
        }

        public async Task<int> GetFlashcardCountByLessonIdAsync(int lessonId)
        {
            return await _context.Flashcards.CountAsync(f => f.LessonId == lessonId);
        }

    }
}
