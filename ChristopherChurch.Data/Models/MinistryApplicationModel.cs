using System;
namespace ChristopherChurch.Data.Models
{
    public class MinistryApplicationModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public bool Authorization { get; set; }
        public string? SelectedMinistry { get; set; }
    }
}

