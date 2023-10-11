using ChristopherChurch.Data.Models;

namespace ChristopherChurch.Data.DataAccess
{
    public interface IPersonData
    {
        Task<List<PersonModel>> GetPeople();
        Task InsertPerson(PersonModel person);
    }
}