using LanguageLearningAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LanguageLearningAI.Service.Repositories
{
    public class LessonRepository
    {
        private readonly ApplicationDbContext _context;

        public LessonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lesson>> GetLessonsByUserAsync(string userId) => await _context.Lessons.Where(l => l.UserId == userId).ToListAsync();

        public async Task<Lesson> GetByIdAsync(int id) => await _context.Lessons.FindAsync(id) ?? throw new ArgumentNullException("Lesson with given id not found");

        public async Task AddAsync(Lesson lesson)
        {
            await _context.Lessons.AddAsync(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Lesson lesson)
        {
            _context.Lessons.Update(lesson);
            await _context.SaveChangesAsync();
        }
    }
}
