using Flight_eBooking.Areas.Identity.Data;
using Flight_eBooking.Data.Enums;
using Flight_eBooking.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Flight_eBooking.Core.ViewModels
{
    public class CreateFlightViewModel
    {

        [Required(ErrorMessage = "Departure Date and Time is required!")]
        [Display(Name = "Departure Date")]
        public DateTime? DepartureDate { get; set; }

        [Required(ErrorMessage = "Flight Class is required!")]
        [Display(Name = "Flight Class")]
        public FlightClass? FlightClass { get; set; }

        [Required(ErrorMessage = "Ticket Price is required!")]
        [Display(Name = "Ticket Price")]
        public float? TicketPrice { get; set; }

        [Required(ErrorMessage = "Number of seats must be more then 0!")]
        [Range(0, 1000, ErrorMessage = "There can only be from 0 to 1000 available seats")]
        [Display(Name = "Number of Seats - Available")]
        public int? Seats { get; set; }

        [Required(ErrorMessage = "Departure destination is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "Departure destination must exist!")]
        public int? DestinationDepartureId { get; set; }

        [Required(ErrorMessage = "Arrival destination is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "Arrival destination must exist!")]
        public int? DestinationArrivalId { get; set; }

    }
}
