using Flight_eBooking.Core.IRepositories;
using Flight_eBooking.Core.Repositories;

namespace Flight_eBooking.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository User { get; }

        public IRoleRepository Role { get;  }

        public IFlightRepository Flight { get; }

        public IDestinationRepository Destination { get; }

        public UnitOfWork(IUserRepository user, IRoleRepository role, IFlightRepository flight, IDestinationRepository destination)
        {
            User = user;
            Role = role;
            Flight = flight;
            Destination = destination;
        }

    }
}
