using System;
using Npgsql;
using System.Data;
using Dapper;
using ChristopherChurch.Data.DbAccess;
using ChristopherChurch.Data.Models;


namespace ChristopherChurch.Data.DataAccess
{
    public class EventsData : IEventsData
    {
        private readonly SqlDataAccess _db;

        public EventsData(SqlDataAccess db)
        {
            _db = db;
        }
        public async Task<List<EventsData>> GetAllEvents()
        {
            string sql = "SELECT event_id, event_name, description, event_date FROM events";
            return await _db.LoadData<EventsData, dynamic>(sql, new { });
        }

        /* public async Task<EventsData> GetEventById(int eventId)
         {
             string sql = "SELECT event_id, event_name, description, event_date FROM events WHERE event_id = @EventId";
             return await _db.LoadData<EventsData>(sql, eventId);
         }
        */
        public async Task AddEvent(EventsData eventData)
        {
            string sql = "INSERT INTO events (event_id, event_name, description, event_date) VALUES (@EventId, @EventName, @Description, @EventDate)";
            await _db.SaveData(sql, eventData);
        }

        public async Task UpdateEvent(EventsData eventData)
        {
            string sql = "UPDATE events SET event_id = @EventId, event_name = @EventName, description = @Description, event_date = @EventDate WHERE event_id = @EventId";
            await _db.SaveData(sql, eventData);
        }

        public async Task DeleteEvent(int eventId)
        {
            string sql = "DELETE FROM events WHERE event_id = @EventId";
            await _db.SaveData(sql, eventId);
        }
    }
}
