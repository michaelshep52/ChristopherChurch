namespace ChristopherChurch.Data.Services
{
    public interface IEmailService
    {
        Task SendEmailWithAttachment(byte[] attachment);
        Task SendPrayerRequestAsync(string name, string request);
    }
}