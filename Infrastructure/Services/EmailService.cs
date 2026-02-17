using System.Net;
using System.Net.Mail;
using Domain.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public Task SendAsync(string to, string subject, string body)
    {
        throw new NotImplementedException();
    }

    public async Task SendEmailConfirmationAsync(string email, string fullname, string confirmationToken)
    {
        var confirmationLink = $"http://localhost:5073/api/auth/confirm-email?email={email}&token={confirmationToken}";
        
        var subject = "Подтверждение Email - Learning Platform";
        var body = $@"
            <h2>Привет, {fullname}!</h2>
            <p>Спасибо за регистрацию на Learning Platform.</p>
            <p>Пожалуйста, подтвердите свой email, нажав на ссылку ниже:</p>
            <a href='{confirmationLink}'>Подтвердить Email</a>
            <p>Или скопируйте эту ссылку в браузер:</p>
            <p>{confirmationLink}</p>
            <br>
            <p>Если вы не регистрировались на нашей платформе, просто проигнорируйте это письмо.</p>
        ";

        await SendEmailAsync(email, subject, body);
    }

    public async Task SendPasswordResetAsync(string email, string resetToken)
    {
        var resetLink = $"http://localhost:5073/api/auth/reset-password?email={email}&token={resetToken}";
        
        var subject = "Восстановление пароля - Learning Platform";
        var body = $@"
            <h2>Восстановление пароля</h2>
            <p>Вы запросили восстановление пароля.</p>
            <p>Нажмите на ссылку ниже для сброса пароля:</p>
            <a href='{resetLink}'>Сбросить пароль</a>
            <p>Или скопируйте эту ссылку в браузер:</p>
            <p>{resetLink}</p>
            <br>
            <p>Если вы не запрашивали сброс пароля, просто проигнорируйте это письмо.</p>
        ";

        await SendEmailAsync(email, subject, body);
    }

    private async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        using var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password)
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_emailSettings.Email, "Learning Platform"),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(toEmail);

        await client.SendMailAsync(mailMessage);
    }
}