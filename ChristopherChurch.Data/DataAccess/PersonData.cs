using System;
using ChristopherChurch.Data.Interfaces;
using ChristopherChurch.Data.Models;

namespace ChristopherChurch.Data.DataAccess
{
    public class PersonData : IPersonData
    {
        private readonly ISqlDataAccess _db;

        public PersonData(ISqlDataAccess db)
        {
            _db = db;
        }
        public Task<List<PersonModel>> GetPeople()
        {
            string sql = "select first_name, last_name, gender from persons";

            return _db.LoadData<PersonModel, dynamic>(sql, new { });
        }

        public Task InsertPerson(PersonModel person)
        {
            string sql = @"insert into persons (first_name, last_name, gender)
                                     values (@first_name, @last_name, @gender);";

            return _db.SaveData(sql, person);
        }
    }
}

