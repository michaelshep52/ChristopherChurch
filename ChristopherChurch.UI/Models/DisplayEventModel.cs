using System;
using System.ComponentModel.DataAnnotations;

namespace ChristopherChurch.UI.Models
{
    public class DisplayEventModel
    {
        public int EventId { get; set; }

        [Required(ErrorMessage = "Event Name is Required.")]
        [StringLength(50, ErrorMessage = "Event Name is too long.")]
        [MinLength(2, ErrorMessage = "Event Name is too hort.")]
        public string EventName { get; set; } = "";

        [Required(ErrorMessage = "Event Description is Required.")]
        [StringLength(500, ErrorMessage = "Event Description is too long. Please keep it below 500 letters.")]
        [MinLength(10, ErrorMessage = "Event Description is too hort.")]
        public string Description { get; set; } = "";

        [Required(ErrorMessage = "Event Description is Required.")]
        public DateTime EventDate { get; set; }

    }
}

