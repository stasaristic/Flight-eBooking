using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_eBooking.Models
{
    public class Destination
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Destination Name")]
        public string NameDest { get; set; }

        [Display(Name = "Destination Image URL")]
        public string ImageDest { get; set; }

        [Display(Name = "Destination Description")]
        public string DescDest { get; set; }
        

        [InverseProperty("DestinationDeparture")]
        public List<Flight> FlightsDeparture { get; set; }

        [InverseProperty("DestinationArrival")]
        public List<Flight> FlightsArrival { get; set; }
    }
}
