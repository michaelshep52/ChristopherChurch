using ChristopherChurch.Data.Models;

namespace ChristopherChurch.Data.DataAccess
{
    public interface IEventsData
    {
        Task<int> AddEvent(EventModel eventModel);
        Task DeleteEvent(int eventId);
        Task<List<EventModel>> GetAllEvents();
        Task UpdateEvent(EventModel eventModel);
    }
}