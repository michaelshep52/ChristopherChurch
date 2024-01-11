using ChristopherChurch.Data.Models;

namespace ChristopherChurch.Data.DataAccess
{
    public interface IEventsData
    {
        Task<int> AddEvent(EventModel eventModel);
        Task DeleteEvent(string eventName);
        Task<List<EventModel>> GetAllEvents();
        Task<EventModel> GetEventByName(string eventName);
        Task UpdateEvent(EventModel eventModel);
    }
}
