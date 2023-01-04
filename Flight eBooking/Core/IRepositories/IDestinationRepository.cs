using Flight_eBooking.Core.Repositories;
using Flight_eBooking.Models;

namespace Flight_eBooking.Core.IRepositories
{
    public interface IDestinationRepository : IRepository<Destination>
    {
        IEnumerable<Destination> GetAll();
        Destination GetById(int? id);
        void InsertDestination(Destination destination);
        void DeleteDestination(int id);
        Destination UpdateDestination(Destination destination);
    }
}
