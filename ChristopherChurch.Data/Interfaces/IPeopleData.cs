using ChristopherChurch.Data.Models;

namespace ChristopherChurch.Data.Interfaces
{
    public interface IPeopleData
    {
        Task<List<PersonModel>> GetPeople();
        Task InsertPerson(PersonModel person);
    }
}