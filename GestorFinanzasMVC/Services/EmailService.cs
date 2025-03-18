using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;

namespace GestorFinanzasMVC.Services
{
    public class EmailService
    {
        private readonly string _emailUser;
        private readonly string _emailPassword;

        public EmailService(IConfiguration configuration)
        {
            _emailUser = configuration["EmailSettings:User"];
            _emailPassword = configuration["EmailSettings:Password"];
        }


        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Gestor Finanzas", "correo@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("html") { Text = message };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_emailUser, _emailPassword);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}