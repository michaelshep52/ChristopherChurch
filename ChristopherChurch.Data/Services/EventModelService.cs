using System.Linq;
using ChristopherChurch.Data.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using ChristopherChurch.Data.DbAccess;
using Microsoft.Extensions.Logging;
using static Dapper.SqlMapper;

namespace ChristopherChurch.Data.Services
{
    public class EventModelService : IEventModelService
    {
        private readonly INpgsqlDataAccess _dataAccess;

        public EventModelService(INpgsqlDataAccess dataAccess)
        {
            // _dataAccess = new NpgsqlDataAccess<EventModel>(connectionString);
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<EventModel>> GetAllEventsAsync()
        {
            try
            {
               // string sql = "select * from events";
               // return await _dataAccess.GetAllAsync<EventModel, dynamic>(sql, new EventModel { });
               return await _dataAccess.GetAllAsync<EventModel>("events");

            }
            catch (AggregateException ae)
            {
                throw ae;
            }
        }

        public async Task<EventModel> GetEventByIdAsync(int eventId)
        {
            try
            {
                return await _dataAccess.GetByIdAsync<EventModel>("events",eventId);
            }
            catch (AggregateException ae)
            {
                throw ae;
            }
        }

        public async Task<int> AddEventAsync(EventModel eventModel)
        {
            try
            {
                return await _dataAccess.InsertAsync("events", eventModel);
            }
            catch (AggregateException ae)
            {
                throw ae;
            }
        }

        public async Task<int> UpdateEventAsync(EventModel eventModel)
        {
            try
            {
                return await _dataAccess.UpdateAsync("events", eventModel);
            }
            catch (AggregateException ae)
            {
                throw ae;
            }
        }

        public async Task<int> DeleteEventAsync(int eventId)
        {
            try
            {
                return await _dataAccess.DeleteAsync("events", eventId);
            }
            catch (AggregateException ae)
            {
                throw ae;
            }

        }
    }

}