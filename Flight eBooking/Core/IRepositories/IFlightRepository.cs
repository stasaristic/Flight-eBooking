using Flight_eBooking.Models;

namespace Flight_eBooking.Core.IRepositories
{
    public interface IFlightRepository 
    {
        Task<IEnumerable<Flight>> GetFlights();
        void InsertFlight(Flight flight);
        Task DeleteAsync(int id);
        Flight UpdateFlight(Flight flight);
        String FlightNameGenerator(int DepId, int ArrId);
        Task<Flight> GetFlightAsync(int id);
    }
}
