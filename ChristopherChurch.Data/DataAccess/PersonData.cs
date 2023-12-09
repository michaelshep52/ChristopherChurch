using System;
using ChristopherChurch.Data.DbAccess;
using ChristopherChurch.Data.Models;
using Dapper;

namespace ChristopherChurch.Data.DataAccess
{
    public class PersonData : IPersonData
    {
        private readonly ISqlDataAccess _db;

        public PersonData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<List<PersonModel>> GetPeople()
        {
            string sql = "select * from persons";
            return await _db.LoadData<PersonModel, dynamic>(sql, new { });
        }

        public async Task InsertPerson( PersonModel person)
        {
            string sql = @"insert into persons (person_id, first_name, last_name, gender, email_address)
                                    values (@person_id, @first_name, @last_name, @gender, @email_address);";
            await _db.SaveData(sql, person);
        }
    }
}

