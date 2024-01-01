using System;
using System.ComponentModel.DataAnnotations;

namespace ChristopherChurch.UI.Models
{
    public class DisplayMinistryApplicationModel
    {
        [Required(ErrorMessage = "First Name is Required.")]
        [StringLength(20, ErrorMessage = "First Name is too long.")]
        [MinLength(2, ErrorMessage = "First Name is too short.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required.")]
        [StringLength(20, ErrorMessage = "Last Name is too long.")]
        [MinLength(2, ErrorMessage = "Last Name is too short.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is Required.")]
        [EmailAddress(ErrorMessage = "Invalid Email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Address is Required.")]
        [StringLength(200, ErrorMessage = "Address is too long.")]
        [MinLength(2, ErrorMessage = "Address is too short.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Invalid characters in the address.")]
        public string? Address { get; set; }

        [StringLength(200, ErrorMessage = "Address is too long.")]
        [MinLength(2, ErrorMessage = "Address is too short.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Invalid characters in the address.")]
        public string? Address2 { get; set; }

        [Required(ErrorMessage = "City is Required.")]
        [StringLength(200, ErrorMessage = "City is too long.")]
        [MinLength(2, ErrorMessage = "City is too short.")]
        public string? City { get; set; }

        [Required(ErrorMessage = "State is Required.")]
        public string? State { get; set; }

        [Required(ErrorMessage = "Zip Code is Required.")]
        [StringLength(5, ErrorMessage = "Zip Code is too long.")]
        [MinLength(5, ErrorMessage = "Zip Code is too short.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid characters in the Zip.")]
        public string? Zip { get; set; }


        [Required(ErrorMessage = "Authorization is Required.")]
        public bool Authorization { get; set; }

        [Required(ErrorMessage = "You must select a Ministry.")]
        public string? SelectedMinistry { get; set; }
    }
}

