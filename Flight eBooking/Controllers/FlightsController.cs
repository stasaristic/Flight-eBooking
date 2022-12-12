using Flight_eBooking.Areas.Identity.Data;
using Flight_eBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Flight_eBooking.Controllers
{
    public class FlightsController : Controller
    {
        Data.DbFunctions Conn;
        private readonly ApplicationDbContext _context;
        List<DataRow> flights = new List<DataRow>();

        public FlightsController(ApplicationDbContext context)
        {
            _context = context;
            Conn = new Data.DbFunctions();
        }
        public async Task<IActionResult> Index()
        {
            FetchData();
            var allFlights = await _context.Flights.ToListAsync();
            return View(flights);
        }

        private void FetchData()
        {
            if (flights.Count > 0) { flights.Clear(); }
            try
            {
                string query = "SELECT Flights.Id, Flights.FlightName, Flights.DepartureDate, Flights.FlightClass, Flights.TicketPrice, Flights.Seats," +
                    "DestDeparture.NameDest AS DepartureName, DestArrival.NameDest AS ArrivalName " +
                    "FROM Flights " +
                    "JOIN Destinations AS DestDeparture ON Flights.DestinationDepartureId = DestDeparture.Id " +
                    "JOIN Destinations AS DestArrival ON Flights.DestinationArrivalId = DestArrival.Id";
                DataTable dt = Conn.GetData(query);
                foreach (DataRow dr in dt.Rows)
                {
                    flights.Add(dr);
                }
                
            }
            catch (Exception ex) { throw; }
            
        }
    }
}
