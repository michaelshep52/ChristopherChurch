using System;
using System.ComponentModel.DataAnnotations;

namespace ChristopherChurch.UI.Models
{
    public class DisplayPersonModel
    {
        public int PersonId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First Name is too long.")]
        [MinLength(2, ErrorMessage = "First Name is too hort.")]
        public  string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last Name is too long.")]
        [MinLength(2, ErrorMessage = "Last Name is too hort.")]
        public  string LastName { get; set; }

        [Required]
        [StringLength(7)]
        public  string Gender { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}

