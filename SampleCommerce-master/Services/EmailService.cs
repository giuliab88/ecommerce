using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace SampleCommerce.Services
{
    public interface IEmailService
    {
        Task SendAsync(string toEmail, string toName, string subject, string htmlBody);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendAsync(string toEmail, string toName, string subject, string htmlBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(
                _config["Email:SenderName"]!,
                _config["Email:SenderEmail"]!));
            message.To.Add(new MailboxAddress(toName, toEmail));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = htmlBody };

            using var client = new SmtpClient();
            await client.ConnectAsync(
                _config["Email:SmtpHost"]!,
                int.Parse(_config["Email:SmtpPort"]!),
                SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(
                _config["Email:Username"]!,
                _config["Email:Password"]!);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
