using Flight_eBooking.Models;

namespace Flight_eBooking.Core.ViewModels
{
    public class TicketSiteViewModel
    {
        public IEnumerable<Flight> Flight { get; set; }
        public IEnumerable<Destination> Destination { get; set; }
    }
}
