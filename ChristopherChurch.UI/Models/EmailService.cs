using System.Net.Mail;

namespace ChristopherChurch.UI.Models
{
    public interface IEmailService
    {
        Task SendPrayerRequestAsync(string toEmail, string subject, string name, string request);
    }

    // EmailService.cs

    public class EmailService : IEmailService
    {
        public async Task SendPrayerRequestAsync(string toEmail, string subject, string name, string request)
        {
            using (var client = new SmtpClient("your-smtp-server"))
            {
                // Configure SMTP settings
                var message = new MailMessage
                {
                    Subject = subject,
                    Body = $"Requester: {name}\n\n{request}",
                    IsBodyHtml = false,
                    // Add more email configuration as needed
                };

                message.From = new MailAddress("michaelshepherd52@hotmail.com");
                message.To.Add(toEmail);

                await client.SendMailAsync(message);
            }
        }
    }

}

