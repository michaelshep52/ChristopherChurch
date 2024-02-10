﻿using System;
namespace ChristopherChurch.Data.Models
{
    public class Auth0Settings
    {
        public string? Domain { get; set; }
        public string? ClientId { get; set; }
        public string? RedirectUri { get; set; }
        public string? PostLogoutRedirectUri { get; set; }
    }
}

