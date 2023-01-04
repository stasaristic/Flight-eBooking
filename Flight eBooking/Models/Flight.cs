using Flight_eBooking.Data.Enums;
using Flight_eBooking.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_eBooking.Models
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Flight Name")]
        public string FlightName { get; set; }
        [Required]
        [Display(Name = "Departure Date")]
        public DateTime DepartureDate { get; set; }
        [Required]
        [Display(Name = "Flight Class")]
        public FlightClass FlightClass { get; set; }
        [Required]
        [Display(Name = "Ticket Price")]
        public float TicketPrice { get; set; }
        [Required]
        [Display(Name = "Number of Seats - Available")]
        public int Seats { get; set; }

        //Relationships
        //public List<Flight_Destination> Flights_Destinations { get; set; }

        [Required]
        [ForeignKey("DestinationDepartureId")]
        public  Destination DestinationDeparture { get; set; }
        public int? DestinationDepartureId { get; set; }

        [Required]
        [ForeignKey("DestinationArrivalId")]
        public  Destination DestinationArrival { get; set; }
        public int? DestinationArrivalId { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}
