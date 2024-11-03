using LanguageLearningAI.Core.Dtos;
using Microsoft.AspNetCore.Identity;

namespace LanguageLearningAI.Core.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUser(RegisterDto registerDto);
        Task<SignInResult> LoginUser(LoginDto loginDto);
    }
}