using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Domain.Entities;

namespace LanguageLearningAI.Core.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterUser(RegisterDto registerDto);
        Task<User> LoginUser(LoginDto loginDto);
    }
}