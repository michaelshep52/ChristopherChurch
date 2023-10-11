using System;
namespace ChristopherChurch.Data.Models
{
    public class PersonModel
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Gender { get; set; }
        public DateTime DateofBirth { get; set; }
    }
}

