using Microsoft.Extensions.Configuration;
using MimeKit;

namespace ChristopherChurch.Data.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       
        public async Task SendPrayerRequestAsync(string name, string request)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_configuration["EmailSettings:SenderName"], _configuration["EmailSettings:SenderEmail"]));
                message.To.Add(new MailboxAddress(_configuration["EmailSettings:RecipientName"], _configuration["EmailSettings:RecipientEmail"]));
                message.Subject = "Prayer Request";


                var builder = new BodyBuilder();
                builder.TextBody =
                    $"Prayer Request \n\nFrom: {name}\n\n{request}";

                message.Body = builder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:SmtpPort"]!), bool.Parse(_configuration["EmailSettings:UseSsl"]!));
                    await client.AuthenticateAsync(_configuration["EmailSettings:SenderEmail"], _configuration["EmailSettings:Password"]);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex}");
                throw;
            }
        }

        public async Task SendEmailWithAttachment(byte[] attachment)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_configuration["EmailSettings:SenderName"], _configuration["EmailSettings:SenderEmail"]));
                message.To.Add(new MailboxAddress(_configuration["EmailSettings:RecipientName"], _configuration["EmailSettings:RecipientEmail"]));
                message.Subject = "Ministry Application Form Submission";

                var builder = new BodyBuilder();

                builder.TextBody = "Ministry Application Form.";
                builder.Attachments.Add("MinistryApplicationForm.pdf", attachment, ContentType.Parse("application/pdf"));

                message.Body = builder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:SmtpPort"]!), bool.Parse(_configuration["EmailSettings:UseSsl"]!));
                    await client.AuthenticateAsync(_configuration["EmailSettings:SenderEmail"], _configuration["EmailSettings:Password"]);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex}");
                throw;
            }
        }
    }

}

