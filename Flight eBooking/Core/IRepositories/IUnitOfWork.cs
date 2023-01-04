using Flight_eBooking.Core.IRepositories;

namespace Flight_eBooking.Core.Repositories
{
    public interface IUnitOfWork //: IDisposable
    {
        IUserRepository User { get; }

        IRoleRepository Role { get; }

        IFlightRepository Flight { get; }

        IDestinationRepository Destination { get; }
    }
}
