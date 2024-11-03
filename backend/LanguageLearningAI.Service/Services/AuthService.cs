using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Core.Services;
using LanguageLearningAI.Domain.Entities;
using LanguageLearningAI.Domain.Repositories;

namespace LanguageLearningAI.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterUser(RegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(registerDto.Email);
            if (existingUser != null) return false;

            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            await _userRepository.AddUserAsync(user);
            return await _userRepository.SaveChangesAsync();
        }

        public async Task<User> LoginUser(LoginDto loginDto)
        {
            return await _userRepository.GetUserByEmailAsync(loginDto.Email);
        }
    }
}