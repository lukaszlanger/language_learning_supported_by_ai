using LanguageLearningAI.Domain.Entities;

namespace LanguageLearningAI.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<User>> GetUsersAsync();
    }
}