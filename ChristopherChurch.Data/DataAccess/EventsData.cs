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
        private readonly ISqlDataAccess _db;

        public EventsData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<List<EventModel>> GetAllEvents()
        {
            string sql = "SELECT event_id, event_name, description, event_date FROM events";
            return await _db.LoadData<EventModel, dynamic>(sql, new { });
        }

        public async Task<int> AddEvent(EventModel eventModel)
        {
            string sql = @"INSERT INTO events (event_name, description, event_date) " +
                         "VALUES (@EventName, @Description, @EventDate)";
             await _db.SaveData(sql, eventModel);
            return 0;
        }

        public async Task UpdateEvent(EventModel eventModel)
        {
            string sql = @"UPDATE events SET event_name = @EventName, description = @Description, event_date = @EventDate WHERE event_id = @EventId";
            await _db.SaveData(sql, eventModel);
        }

        public async Task DeleteEvent(int eventId)
        {
            Console.WriteLine($"Deleting event with ID: {eventId}");

            string sql = @"DELETE FROM events WHERE event_id = @EventId";
            await _db.SaveData(sql, new { EventId = eventId });
        }
    }

}
