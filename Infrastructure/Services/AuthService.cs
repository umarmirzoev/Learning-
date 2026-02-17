using Domain.DTOs;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDBContext _context;
    private readonly IJwtService _jwtService;
    private readonly IEmailService _emailService;

    public AuthService(ApplicationDBContext context, IJwtService jwtService, IEmailService emailService)
    {
        _context = context;
        _jwtService = jwtService;
        _emailService = emailService;
    }

    public async Task<AuthResponseDto> Register(RegisterDto registerDto)
    {
        // Проверяем существует ли пользователь с таким email
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == registerDto.Email);

        if (existingUser != null)
        {
            throw new Exception("User with this email already exists");
        }

        // Хешируем пароль
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

        // Генерируем токен подтверждения email
        var emailConfirmationToken = Guid.NewGuid().ToString();

        // Создаём нового пользователя
        var newUser = new User
        {
            Fullname = registerDto.Fullname,
            Email = registerDto.Email,
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow,
            Role = registerDto.Role,
            IsEmailConfirmed = false,
            EmailConfirmationToken = emailConfirmationToken
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        // Отправляем email подтверждение
        try
        {
            await _emailService.SendEmailConfirmationAsync(
                newUser.Email ?? string.Empty,
                newUser.Fullname ?? string.Empty,
                emailConfirmationToken
            );
        }
        catch (Exception ex)
        {
            // Логируем ошибку, но продолжаем регистрацию
            Console.WriteLine($"Email sending failed: {ex.Message}");
        }

        // Генерируем JWT токен
        var token = _jwtService.GenerateToken(newUser);

        return new AuthResponseDto
        {
            Token = token,
            Email = newUser.Email ?? string.Empty,
            Fullname = newUser.Fullname ?? string.Empty,
            UserId = newUser.Id,
            Role = newUser.Role,
            IsEmailConfirmed = newUser.IsEmailConfirmed
        };
    }

    public async Task<AuthResponseDto> Login(LoginDto loginDto)
    {
        // Ищем пользователя по email
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        if (user == null)
        {
            throw new Exception("Invalid email or password");
        }

        // Проверяем пароль
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);

        if (!isPasswordValid)
        {
            throw new Exception("Invalid email or password");
        }

        // Опционально: можно требовать подтверждение email для входа
        // if (!user.IsEmailConfirmed)
        // {
        //     throw new Exception("Please confirm your email before logging in");
        // }

        // Генерируем JWT токен
        var token = _jwtService.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email ?? string.Empty,
            Fullname = user.Fullname ?? string.Empty,
            UserId = user.Id,
            Role = user.Role,
            IsEmailConfirmed = user.IsEmailConfirmed
        };
    }

    public async Task<bool> ConfirmEmail(string email, string token)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email && u.EmailConfirmationToken == token);

        if (user == null)
        {
            return false;
        }

        user.IsEmailConfirmed = true;
        user.EmailConfirmationToken = null; // Удаляем токен после подтверждения

        await _context.SaveChangesAsync();
        return true;
    }
}