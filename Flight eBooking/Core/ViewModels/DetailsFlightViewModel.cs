using Flight_eBooking.Data.Enums;
using Flight_eBooking.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Flight_eBooking.Core.ViewModels
{
    public class DetailsFlightViewModel
    {
        [ValidateNever]
        public Flight? Flight { get; set; }
        //public Reservation Reservation { get; set; }

        [Required(ErrorMessage = "Number of seats reserved is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "There must be more than 0 seats reserved")]
        [Display(Name = "Number of reserved seats")]
        public int NumberOfSeats { get; set; }
    }
}
