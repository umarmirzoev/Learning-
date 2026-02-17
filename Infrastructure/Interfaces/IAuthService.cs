using Domain.DTOs;

namespace Infrastructure.Services;

public interface IAuthService
{
    Task<AuthResponseDto> Register(RegisterDto registerDto);
    Task<AuthResponseDto> Login(LoginDto loginDto);
    Task<bool> ConfirmEmail(string email, string token);
}