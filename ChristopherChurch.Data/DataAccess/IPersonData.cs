using ChristopherChurch.Data.Models;

namespace ChristopherChurch.Data.DataAccess
{
    public interface IPersonData
    {
        Task<List<PersonModel>> GetPeople();
        Task InsertPerson(PersonModel person);
        /*Task DeletePerson(int id);
        Task<PersonModel?> GetPerson(int id);
        Task<IEnumerable<PersonModel>> GetPersons();
        Task InsertPerson(PersonModel person);
        Task UpdatePerson(PersonModel person);*/
    }
}