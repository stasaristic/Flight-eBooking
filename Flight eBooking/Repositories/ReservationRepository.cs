using Flight_eBooking.Areas.Identity.Data;
using Flight_eBooking.Core.IRepositories;
using Flight_eBooking.Core.Repositories;
using Flight_eBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace Flight_eBooking.Repositories
{
    public class ReservationRepository : IReservationRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task DeleteAsync(int id)
        {
            Reservation reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
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

        public async Task<Reservation> GetReservationAsync(int id)
        {
            var reservation = await _context.Reservations.Include(r => r.User).Where(r => r.UserId == r.User.Id)
                                        .Include(r => r.Flight).Where(r => r.FlightId == r.Flight.Id)
                                        .FirstOrDefaultAsync(r => r.Id == id);
            return reservation;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync()
        {
            return await _context.Reservations.Include(r => r.User).Where(r => r.UserId == r.User.Id)
                                        .Include(r => r.Flight).Where(r => r.FlightId == r.Flight.Id).ToListAsync();
        }

        public void InsertReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }

        public Reservation UpdateReservation(Reservation reservation)
        {
            _context.Update(reservation);
            _context.SaveChanges();

            return reservation;
        }

    }
}
