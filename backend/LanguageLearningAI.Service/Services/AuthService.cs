using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace LanguageLearningAI.Service.Services
{
    public class AuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthService(
            SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterDto registerDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User already exists." });
            }

            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                NativeLanguage = registerDto.NativeLanguage
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            return result;
        }

        public async Task<SignInResult> LoginUserAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return SignInResult.Failed;
            return await _signInManager.PasswordSignInAsync(user, loginDto.Password, isPersistent: false, lockoutOnFailure: false);
        }

        public async Task<UserDto> GetUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                NativeLanguage = user.NativeLanguage
            };
        }
    }
}