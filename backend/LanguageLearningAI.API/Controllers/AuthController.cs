using Microsoft.AspNetCore.Mvc;
using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Service.Services;

namespace LanguageLearningAI.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerDto)
        {
            var result = await _authService.RegisterUserAsync(registerDto);
            if (result.Succeeded)
            {
                return Ok("User registered successfully.");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            var result = await _authService.LoginUserAsync(loginDto);
            if (result.Succeeded)
            {
                return Ok("Login successful.");
            }
            return Unauthorized("Invalid login attempt.");
        }

        [HttpGet("getUser/{email}")]
        public async Task<UserDto> GetUserAsync(string email)
        {
            return await _authService.GetUserAsync(email) ?? throw new Exception("User not found");
        }
    }
}