using Alchemy.Domain.Models;
using Alchemy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Alchemy.Infrastructure.Repositories
{
    public class MasterRepository : GenericRepository<Master>, IMasterRepository
    {
        private readonly AlchemyDbContext _context;

        public MasterRepository(AlchemyDbContext context) : base(context)
        {
            _context = context;
        }
        
    }
}
