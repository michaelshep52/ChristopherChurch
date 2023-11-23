namespace ChristopherChurch.Data.Services
{
    public interface IEmailService
    {
        Task SendPrayerRequestAsync(string name, string request);
    }
}