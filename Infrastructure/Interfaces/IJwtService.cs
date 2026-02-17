namespace Infrastructure.Services;

public interface IJwtService
{
    string GenerateToken(User user);
}