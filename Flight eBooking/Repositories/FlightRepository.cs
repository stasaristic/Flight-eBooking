using Flight_eBooking.Areas.Identity.Data;
using Flight_eBooking.Core.IRepositories;
using Flight_eBooking.Core.Repositories;
using Flight_eBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace Flight_eBooking.Repositories
{
    public class FlightRepository : IFlightRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public FlightRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void DeleteFlight(int id)
        {
            Flight flight = _context.Flights.Find(id);
            _context.Flights.Remove(flight);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public  Flight GetFlight(int id)
        {
            return _context.Flights.Include(f => f.DestinationDeparture).Where(f => f.DestinationDepartureId == f.DestinationDeparture.Id)
                                        .Include(f => f.DestinationArrival).Where(f => f.DestinationArrivalId == f.DestinationArrival.Id)
                                        .FirstOrDefault(f => f.Id == id);
        }

        public async Task<IEnumerable<Flight>> GetFlights()
        {
            return await _context.Flights.Include(f => f.DestinationDeparture).Where(f => f.DestinationDepartureId == f.DestinationDeparture.Id)
                                        .Include(f => f.DestinationArrival).Where(f => f.DestinationArrivalId == f.DestinationArrival.Id).ToListAsync();
        }

        public void InsertFlight(Flight flight)
        {
            _context.Flights.Add(flight);
            _context.SaveChanges();
        }

        public Flight UpdateFlight(Flight flight)
        {
            _context.Update(flight);
            _context.SaveChanges();

            return flight;
        }

        public String FlightNameGenerator(int DepId, int ArrId)
        {
            String FlightName = "";

            String DepartureName = _context.Destinations.FirstOrDefault(f => f.Id == DepId).NameDest;
            String ArrivalName = _context.Destinations.FirstOrDefault(f => f.Id == ArrId).NameDest;

            FlightName = DepartureName + " - " + ArrivalName;

            return FlightName;
        }
    }
}
