using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using SweetDebt.Models;

namespace SweetDebt.Service
{
    public class NotificationService
    {
        private const string urlAzure = "\nhttps://sweetdebt-bkekdqdwb5f8hca8.germanywestcentral-01.azurewebsites.net/";
        private readonly MimeMessage _email;
        private readonly SmtpClient _smtpClient;

        public NotificationService()
        {
            _email = new MimeMessage();
            _smtpClient = new SmtpClient();
        }
        public async Task SendNotificationAsync(MyTransaction transaction)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress($"Zloděj", "pokusomyl007@email.cz"));
            email.To.Add(new MailboxAddress("Čudlík", "lstrnad13@gmail.com"));
            email.Subject = "Pozor zloděj !";

            email.Body = new TextPart("plain")
            {
                Text = GetText(transaction) + urlAzure + " tady se koukneš na stav"
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
