using Flight_eBooking.Data.Enums;
using Flight_eBooking.Models;

namespace Flight_eBooking.Core.ViewModels
{
    public class DetailsFlightViewModel
    {
        public Flight Flight { get; set; }
        public Reservation Reservation { get; set; }
    }
}
