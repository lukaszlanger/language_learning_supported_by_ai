using LanguageLearningAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LanguageLearningAI.Service.Repositories
{
    public class QuizRepository
    {
        private readonly ApplicationDbContext _context;

        public QuizRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Quiz>> GetAllAsync()
        {
            return await _context.Quizzes
                .Include(q => q.Questions)
                .ToListAsync();
        }

        public async Task<IEnumerable<Quiz>> GetAllByLessonIdAsync(int lessonId)
        {
            return await _context.Quizzes
                .Where(q => q.LessonId == lessonId)
                .Include(q => q.Questions)
                .ToListAsync();
        }


        public async Task<Quiz> GetByIdAsync(int id)
        {
            return await _context.Quizzes
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task AddAsync(Quiz quiz)
        {
            await _context.Quizzes.AddAsync(quiz);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Quiz quiz)
        {
            _context.Quizzes.Update(quiz);
            await _context.SaveChangesAsync();
        }
    }
}