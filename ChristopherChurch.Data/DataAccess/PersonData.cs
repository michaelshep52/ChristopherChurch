using System;
using ChristopherChurch.Data.DbAccess;
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
            string sql = "select * from persons";
            return _db.LoadData<PersonModel, dynamic>(sql, new { });
        }

        public Task InsertPerson( PersonModel person)
        {
            string sql = @"insert into persons (first_name, last_name, gender, date_of_birth)
                                    values (@first_name, @last_name, @gender, @date_of_birth);";
            return _db.SaveData(sql, person);
        }
        /*  public Task<IEnumerable<PersonModel>> GetPersons() =>
              _db.LoadData<PersonModel, dynamic>(storedProcedure: "persons_GetAll", new { });

          public async Task<PersonModel?> GetPerson(int id)
          {
              var results = await _db.LoadData<PersonModel, dynamic>(
                  storedProcedure: "persons_Get",
                  new { Id = id });
              return results.FirstOrDefault();
          }
          public Task InsertPerson(PersonModel person) =>
              _db.SaveData(storedProcedure: "persons_Insert",
                  new { person.FirstName, person.LastName, person.Gender, person.DateofBirth });

          public Task UpdatePerson(PersonModel person) =>
              _db.SaveData(storedProcedure: "persons_Update", person);

          public Task DeletePerson(int id) =>
              _db.SaveData(storedProcedure: "persons_Delete", new { Id = id });
        */

    }
}

