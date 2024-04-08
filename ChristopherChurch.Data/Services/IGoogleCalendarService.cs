using ChristopherChurch.Data.Models;
using Google.Apis.Calendar.v3.Data;

namespace ChristopherChurch.Data.Services
{
    public interface IGoogleCalendarService
    {
        string GetEventAddToGoogleCalendarLink(Event websiteEvent);
        Task<List<EventModel>> GetPublicEventsAsync();
    }
}