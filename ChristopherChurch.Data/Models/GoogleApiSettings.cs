using System;
namespace ChristopherChurch.Data.Models
{
    public class GoogleApiSettings
    {
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? RedirectUri { get; set; }
        public string? TokenPath { get; set; }
    }
}

