using System;
namespace ChristopherChurch.Data.Models
{
    public class AuthResult
    {
        public bool IsAuthorized { get; set; }
        public string? Error { get; set; }
    }

}

