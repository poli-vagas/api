using System.Net;
using System.Net.Mail;
using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Infrastructure.Services;

public class MailService : IMailService
{
    private EmailSettings _settings;

    public MailService(EmailSettings settings)
    {
        _settings = settings;
    }

    public async Task SendEmail(string subject, string to, string body)
    {
        var message = new MailMessage();
        message.Subject = subject;
        message.From = new MailAddress(_settings.Mail, _settings.DisplayName);
        message.To.Add(new MailAddress(to));
        message.IsBodyHtml = true;
        message.Body = body;
        message.IsBodyHtml = true;

        using var smtp = new SmtpClient(_settings.Host, _settings.Port);
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential(_settings.Mail, _settings.Password);
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        await smtp.SendMailAsync(message);
        smtp.Dispose();
    }
}

public class EmailSettings
{
    public string Mail { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Host { get; set; } = default!;
    public int Port { get; set; } = default!;
}
