using Microsoft.AspNetCore.Mvc;
using LanguageLearningAI.Core.Services;
using LanguageLearningAI.Core.Dtos;

namespace LanguageLearningAI.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var success = await _authService.RegisterUser(registerDto);
            if (!success) return BadRequest("User already exists.");

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _authService.LoginUser(loginDto);
            if (user == null) return Unauthorized();

            return Ok("Login successful");
        }
    }
}