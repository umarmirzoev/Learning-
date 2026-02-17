using Domain.Enums;

namespace Domain.DTOs;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Fullname { get; set; } = string.Empty;
    public int UserId { get; set; }
    public UserRole Role { get; set; }
    public bool IsEmailConfirmed { get; set; }
}