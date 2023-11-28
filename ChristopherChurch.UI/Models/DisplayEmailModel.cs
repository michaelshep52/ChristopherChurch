using System;
using System.ComponentModel.DataAnnotations;

namespace ChristopherChurch.UI.Models
{
    public class DisplayEmailModel
    {
        [Required(ErrorMessage = "Full Name is Required.")]
        [StringLength(50, ErrorMessage = "Full Name is too long.")]
        [MinLength(2, ErrorMessage = "Full Name is too hort.")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Prayer Request is Required.")]
        [StringLength(500, ErrorMessage = "Prayer Request is too long. Please stay below 500 letters.")]
        [MinLength(5, ErrorMessage = "Prayer requset is too hort.")]
        public string Request { get; set; } = "";
    }
}

