namespace ChristopherChurch.Data.Services
{
    public interface IGoogleCalendarService
    {
        Task AddEventAsync(string eventName, string userToken);
    }
}