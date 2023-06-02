using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyLeasing.Web.Data.Entities;
using System.Linq;

namespace MyLeasing.Web.Data
{
    public class OwnerRepository : GenericRepository<Owner>, IOwnerRepository
    {
        private readonly DataContext _context;
        public OwnerRepository(DataContext context) : base(context)
        {
            _context = context;            
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Owners.Include(m => m.User);
        }
    }
}
