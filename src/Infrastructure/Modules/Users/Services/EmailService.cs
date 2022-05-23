using Core.Common.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace Infrastructure.Modules.Users.Services;

public interface IEmailService : IScopedService
{
    void Send(string to, string subject, string html, string from = null);
}

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
 public void Send(string to, string subject, string html, string from = null)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(from ?? _configuration["MailSettings:Mail"]));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = html };
        
        using var smtp = new SmtpClient();
        smtp.Connect(_configuration["MailSettings:Host"], int.Parse(_configuration["MailSettings:Port"]), SecureSocketOptions.StartTls);
        smtp.Authenticate(_configuration["MailSettings:Mail"], _configuration["MailSettings:Password"]);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}