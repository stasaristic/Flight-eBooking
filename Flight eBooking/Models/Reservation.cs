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

        public ReservationStatus StatusRes { get; set; }

        [Display(Name = "Number of reserved seats")]
        public int NumberOfSeats { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser User { get; set; }

        public int FlightId { get; set; }
        [ForeignKey("FlightId")]
        [ValidateNever]
        public Flight Flight { get; set; }
    }
}
