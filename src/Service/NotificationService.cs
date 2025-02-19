using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using SweetDebt.Models;

namespace SweetDebt.Service
{
    public class NotificationService
    {
        private readonly MimeMessage _email;
        private readonly SmtpClient _smtpClient;
        private readonly IConfiguration _configuration;

        public NotificationService(IConfiguration configuration)
        {
            _email = new MimeMessage();
            _smtpClient = new SmtpClient();
            _configuration = configuration;
        }
        public async Task SendNotificationAsync(MyTransaction transaction)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress($"Zloděj", _configuration["AppSetting:NotificationEmail:FromEmail"]));
            email.To.Add(new MailboxAddress("Čudlík", _configuration["AppSetting:NotificationEmail:ToEmail"]));
            email.Subject = "Pozor zloděj !";

            email.Body = new TextPart("plain")
            {
                Text = GetText(transaction)
            };

            using (var client = new SmtpClient())
            {

                await client.ConnectAsync(_configuration["AppSetting:NotificationEmail:SmtpClient:SmtpUrl"],Int32.Parse(_configuration["AppSetting:NotificationEmail:SmtpClient:SmtpPort"]),GetSecurity(_configuration["AppSetting:NotificationEmail:SmtpClient:SmtpSecure"]));
                await client.AuthenticateAsync(_configuration["AppSetting:NotificationEmail:SmtpClient:SmtpLogin"], _configuration["AppSetting:NotificationEmail:SmtpClient:SmtpPassword"]);
                await client.SendAsync(email);
                await client.DisconnectAsync(true);

            }

        }
        private string GetText(MyTransaction transaction)
        {
            if (transaction.TypeOfTransaction == TypeOfTransaction.Negative)
                return $"Bylo Ti ukradeno -{transaction.Amount} Kč.";
            else
                return $"Bylo Ti vráceno {transaction.Amount} Kč.";
        }
        private MailKit.Security.SecureSocketOptions GetSecurity(string type)
        {
            if (type is null)
                return MailKit.Security.SecureSocketOptions.None;
            else
            {
                return type == "SSL" ? MailKit.Security.SecureSocketOptions.SslOnConnect : MailKit.Security.SecureSocketOptions.StartTls;
            }
        }
    }
}
