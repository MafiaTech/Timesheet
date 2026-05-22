using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Timesheet.Services;

public class EmailService
{
    private readonly IConfiguration configuration;

    public EmailService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
    {
        var enableEmailSending = bool.TryParse(configuration["EmailSettings:EnableEmailSending"], out var enabled) && enabled;

        if (!enableEmailSending)
        {
            await Task.CompletedTask;
            return;
        }

        var smtpHost = GetRequiredSetting("SmtpHost");
        var smtpPort = GetRequiredPort();
        var smtpUsername = GetRequiredSetting("SmtpUsername");
        var smtpPassword = GetRequiredSetting("SmtpPassword");
        var fromEmail = GetRequiredSetting("FromEmail");
        var fromName = GetRequiredSetting("FromName");

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(fromName, fromEmail));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = subject;
        message.Body = new BodyBuilder
        {
            HtmlBody = htmlBody
        }.ToMessageBody();

        using var smtpClient = new SmtpClient();
        await smtpClient.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls);
        await smtpClient.AuthenticateAsync(smtpUsername, smtpPassword);
        await smtpClient.SendAsync(message);
        await smtpClient.DisconnectAsync(true);
    }

    private string GetRequiredSetting(string key)
    {
        var value = configuration[$"EmailSettings:{key}"];

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException($"EmailSettings:{key} is required.");
        }

        return value;
    }

    private int GetRequiredPort()
    {
        var value = GetRequiredSetting("SmtpPort");

        if (!int.TryParse(value, out var port))
        {
            throw new InvalidOperationException("EmailSettings:SmtpPort must be a valid integer.");
        }

        return port;
    }
}
