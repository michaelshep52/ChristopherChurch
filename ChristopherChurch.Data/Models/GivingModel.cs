using System;
using System.ComponentModel.DataAnnotations;

namespace ChristopherChurch.Data.Models
{
    public class GivingModel
    {
        public int Number { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }
    }
}

