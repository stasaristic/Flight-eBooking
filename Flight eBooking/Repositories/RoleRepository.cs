using Flight_eBooking.Areas.Identity.Data;
using Flight_eBooking.Core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Flight_eBooking.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
