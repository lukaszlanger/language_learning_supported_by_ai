﻿using LanguageLearningAI.Domain.Entities;
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

        public async Task<IEnumerable<Lesson>> GetLessons() => await _context.Lessons.ToListAsync();

        public async Task<IEnumerable<Lesson>> GetLessonsByUserAsync(string userId) => await _context.Lessons.Where(l => l.UserId == userId).ToListAsync();

        public async Task<Lesson> GetByIdAsync(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id) ?? throw new ArgumentNullException(nameof(Lesson));
            return lesson;
        }

        public async Task<int> AddAsync(Lesson lesson)
        {
            await _context.Lessons.AddAsync(lesson);
            await _context.SaveChangesAsync();
            return lesson.Id;
        }

        public async Task UpdateAsync(Lesson lesson)
        {
            _context.Lessons.Update(lesson);
            await _context.SaveChangesAsync();
        }
    }
}
