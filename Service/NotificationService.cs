using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using SweetDebt.Models;

namespace SweetDebt.Service
{
    public class NotificationService
    {
        private readonly IConfiguration _configuration;
        private readonly MimeMessage _email;
        private readonly SmtpClient _smtpClient;
        public string Setting {  get; set; }

        public NotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
            _email = new MimeMessage();
            _smtpClient = new SmtpClient();
            Setting =  _configuration.GetConnectionString("ToEmail");
        }
        public async Task SendNotificationAsync(MyTransaction transaction)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress($"Zloděj", "pokusomyl007@email.cz"));
            email.To.Add(new MailboxAddress("Čudlík", $"{Setting}"));
            email.Subject = "Pozor zloděj !";

            email.Body = new TextPart("plain")
            {
                Text = GetText(transaction)
            };

            using (var client = new SmtpClient())
            {

                await client.ConnectAsync("smtp.seznam.cz", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync("pokusomyl007@email.cz", "FlorianovaLenicka1994");
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
    }
}
