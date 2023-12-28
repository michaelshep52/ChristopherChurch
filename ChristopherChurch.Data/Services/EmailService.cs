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
                message.From.Add(new MailboxAddress("Software Developer", "sdcodemail@gmail.com"));
                message.To.Add(new MailboxAddress("Michael", "michaelshep52@gmail.com"));
                message.Subject = "Prayer Request";

                var builder = new BodyBuilder();
                builder.TextBody =
                    $"Prayer Request \n\nFrom: {name}\n\n{request}";

                message.Body = builder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                    await client.AuthenticateAsync("sdcodemail@gmail.com", "rojz bsvp rnyn pqlu");
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
                message.From.Add(new MailboxAddress("Software Developer", "sdcodemail@gmail.com"));
                message.To.Add(new MailboxAddress("Michael", "michaelshep52@gmail.com"));
                message.Subject = "Ministry Application Form Submission";

                var builder = new BodyBuilder();

                builder.TextBody = "Ministry Application Form.";
                builder.Attachments.Add("MinistryApplicationForm.pdf", attachment, ContentType.Parse("application/pdf"));

                message.Body = builder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                    await client.AuthenticateAsync("sdcodemail@gmail.com", "rojz bsvp rnyn pqlu");
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

