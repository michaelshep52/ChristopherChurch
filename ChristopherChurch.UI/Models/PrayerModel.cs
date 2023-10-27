using System;
using System.ComponentModel.DataAnnotations;

namespace ChristopherChurch.UI.Models
{
    public class PrayerModel
    {
        [Required]
        [EmailAddress]
        public string Email;

        [Required]
        [StringLength(100, ErrorMessage = "Full Name is too long.")]
        [MinLength(4, ErrorMessage = "Full Name is too hort.")]
        public string Name;

        [Required]
        [StringLength(500, ErrorMessage = "Prayer request has exceeded 500 letters and is too long.")]
        [MinLength(10, ErrorMessage = "Prayer request is too hort.")]
        public string Prayer;
    }
}

