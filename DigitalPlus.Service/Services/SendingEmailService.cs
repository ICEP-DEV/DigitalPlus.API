using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DigitalPlus.Service.Interfaces;

namespace DigitalPlus.Service
{
    public class SendingEmailService : ISendEmail
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;

        public SendingEmailService(string smtpServer, int smtpPort, string smtpUser, string smtpPass)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                using (var client = new SmtpClient(_smtpServer, _smtpPort))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                    client.EnableSsl = true; // Important for Gmail

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_smtpUser),
                        Subject = subject,
                        Body = message,
                        IsBodyHtml = true // Set this based on your email content
                    };
                    mailMessage.To.Add(email);

                    await client.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (log or rethrow as needed)
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw;
            }
        }
    }
}
