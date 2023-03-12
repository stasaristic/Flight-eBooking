 using Flight_eBooking.Areas.Identity.Data;
using Flight_eBooking.Data.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_eBooking.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Status of the Reservation must be defined!")]
        public ReservationStatus StatusRes { get; set; }

        [Required(ErrorMessage = "Number of seats reserved is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "There must be more than 0 seats reserved")]
        [Display(Name = "Number of reserved seats")]
        public int NumberOfSeats { get; set; }

        public string UserId { get; set; }
        [Required]
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser User { get; set; }

        public int FlightId { get; set; }
        [Required]
        [ForeignKey("FlightId")]
        [ValidateNever]
        public Flight Flight { get; set; }
    }
}
