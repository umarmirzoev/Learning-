namespace Infrastructure.Services;

public interface IEmailService
{
    Task SendEmailConfirmationAsync(string email, string fullname, string confirmationToken);
    Task SendPasswordResetAsync(string email, string resetToken);
    Task SendAsync(string to, string subject, string body);

}