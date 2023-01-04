using Flight_eBooking.Areas.Identity.Data;
using Flight_eBooking.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Flight_eBooking.Core.ViewModels
{
    public class CreateFlightViewModel
    {
        public Flight Flight { get; set; }
        public IList<SelectListItem> Destinations { get; set; }
    }
}
