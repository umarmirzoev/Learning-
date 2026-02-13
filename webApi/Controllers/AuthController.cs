using Domain.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Регистрация нового пользователя
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            var response = await _authService.Register(registerDto);
            return Ok(new 
            { 
                message = "Registration successful! Please check your email to confirm your account.",
                data = response 
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Вход пользователя
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var response = await _authService.Login(loginDto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Подтверждение email
    /// </summary>
    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
    {
        try
        {
            var result = await _authService.ConfirmEmail(email, token);
            
            if (result)
            {
                return Ok(new { message = "Email confirmed successfully! You can now login." });
            }
            
            return BadRequest(new { message = "Invalid confirmation token or email." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Получить информацию о текущем пользователе (требует авторизации)
    /// </summary>
    [Authorize]
    [HttpGet("me")]
    public IActionResult GetCurrentUser()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
        var fullname = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

        return Ok(new
        {
            userId,
            email,
            fullname,
            role
        });
    }

    /// <summary>
    /// Logout (опционально - для очистки токена на клиенте)
    /// </summary>
    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        return Ok(new { message = "Logged out successfully" });
    }

    /// <summary>
    /// Endpoint только для студентов
    /// </summary>
    [Authorize(Roles = "Student")]
    [HttpGet("student-only")]
    public IActionResult StudentOnly()
    {
        return Ok(new { message = "This is student-only content!" });
    }

    /// <summary>
    /// Endpoint только для инструкторов
    /// </summary>
    [Authorize(Roles = "Instructor")]
    [HttpGet("instructor-only")]
    public IActionResult InstructorOnly()
    {
        return Ok(new { message = "This is instructor-only content!" });
    }

    /// <summary>
    /// Endpoint только для админов
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpGet("admin-only")]
    public IActionResult AdminOnly()
    {
        return Ok(new { message = "This is admin-only content!" });
    }
}