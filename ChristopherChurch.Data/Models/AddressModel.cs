using System;
namespace ChristopherChurch.Data.Models
{
    public class AddressModel
    {
        public required string Address1 { get; set; }
        public  string? Address2 { get; set; }
        public  string? Address3 { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public int Postal { get; set; }
        public required string Country { get; set; }
    }
}

