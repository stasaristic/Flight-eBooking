using Flight_eBooking.Models;

namespace Flight_eBooking.Core.IRepositories
{
    public interface IFlightRepository 
    {
        Task<IEnumerable<Flight>> GetFlights();
        Flight GetFlight(int id);
        void InsertFlight(Flight flight);
        void DeleteFlight(int id);
        Flight UpdateFlight(Flight flight);
        String FlightNameGenerator(int DepId, int ArrId);
    }
}
