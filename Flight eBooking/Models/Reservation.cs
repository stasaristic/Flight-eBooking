using Flight_eBooking.Areas.Identity.Data;
using Flight_eBooking.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_eBooking.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public ReservationStatus StatusRes { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public int FlightId { get; set; }
        [ForeignKey("FlightId")]
        public Flight Flight { get; set; }
    }
}
