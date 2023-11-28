using ChristopherChurch.Data.Models;

namespace ChristopherChurch.Data.Services
{
    public interface IEventModelService
    {
        Task<int> AddEventAsync(EventModel eventModel);
        Task<int> DeleteEventAsync(int eventId);
        Task<IEnumerable<EventModel>> GetAllEventsAsync();
        Task<EventModel> GetEventByIdAsync(int eventId);
        Task<int> UpdateEventAsync(EventModel eventModel);
    }
}