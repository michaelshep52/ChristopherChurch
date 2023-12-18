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
            //AS clause had to be used to properly retrieve all data from table with dapper.
            string sql = "SELECT event_name AS EventName, description AS Description, event_date AS EventDate FROM events";
            return await _db.LoadData<EventModel, dynamic>(sql, new { });
        }

        public async Task<EventModel> GetEventByName(string eventName)
        {
            try
            {
                string sql = "SELECT event_name AS EventName, description AS Description, event_date AS EventDate FROM events WHERE event_name = @EventName";

                EventModel eventModel = await _db.LoadSingleData<EventModel, dynamic>(sql, new { EventName = eventName });

                return eventModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting event by name: {ex.Message}");
                throw;
            }
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
            string sql = @"UPDATE events SET event_name = @EventName, description = @Description, event_date = @EventDate WHERE event_name = @EventName";
            await _db.SaveData(sql, eventModel);
        }

        public async Task DeleteEvent(string eventName)
        {
            Console.WriteLine($"Deleting event with EventName: {eventName}");

            string sql = @"DELETE FROM events WHERE ""event_name"" = @EventName";
            await _db.SaveData(sql, new { EventName = eventName });
        }
    }

}
