using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LanguageLearningAI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var user = new User
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            NativeLanguage = registerDto.NativeLanguage,
            LearningLanguage = registerDto.LearningLanguage,
            DifficultyLevel = registerDto.DifficultyLevel
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);
        if (!result.Succeeded)
        {
            return Unauthorized();
        }

        return Ok("User logged in successfully");
    }
}