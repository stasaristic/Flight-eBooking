using Flight_eBooking.Areas.Identity.Data;
using Flight_eBooking.Core.IRepositories;
using Flight_eBooking.Models;
using System.Linq.Expressions;

namespace Flight_eBooking.Repositories
{
    public class DestinationRepository :  Repository<Destination>, IDestinationRepository
    {
        public DestinationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void DeleteDestination(int id)
        {
            Destination destination = _context.Destinations.Find(id);
            _context.Destinations.Remove(destination);  
        }

        public IEnumerable<Destination> GetAll()
        {
            return _context.Destinations.ToList();
        }

        public Destination GetById(int? id)
        {
            return _context.Destinations.FirstOrDefault(d => d.Id == id);
        }

        public void InsertDestination(Destination destination)
        {
            _context.Destinations.Add(destination);
        }

        public Destination UpdateDestination(Destination destination)
        {
            _context.Update(destination);
            _context.SaveChanges();

            return destination;
        }
    }
}
