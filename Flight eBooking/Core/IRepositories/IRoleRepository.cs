using Flight_eBooking.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace Flight_eBooking.Core.Repositories
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
