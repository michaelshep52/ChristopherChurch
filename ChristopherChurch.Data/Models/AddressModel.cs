using System;
namespace ChristopherChurch.Data.Models
{
    public class AddressModel
    {
        public  string? Address1 { get; set; }
        public  string? Address2 { get; set; }
        public  string? Address3 { get; set; }
        public  string? City { get; set; } 
        public  string? State { get; set; }
        public int Postal { get; set; }
        public  string? Country { get; set; }
    }
}

