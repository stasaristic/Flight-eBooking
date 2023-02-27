using Flight_eBooking.Models;

namespace Flight_eBooking.Core.IRepositories
{
    public interface IReservationRepository 
    {
        Task<IEnumerable<Reservation>> GetReservationsAsync();
        void InsertReservation(Reservation reservation);
        Task DeleteAsync(int id);
        Reservation UpdateReservation(Reservation reservation);
        Task<Reservation> GetReservationAsync(int id);
    }
}
