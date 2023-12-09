namespace ChristopherChurch.Data.DataAccess
{
    public interface IEventsData
    {
        Task AddEvent(EventsData eventData);
        Task DeleteEvent(int eventId);
        Task<List<EventsData>> GetAllEvents();
        Task UpdateEvent(EventsData eventData);
    }
}