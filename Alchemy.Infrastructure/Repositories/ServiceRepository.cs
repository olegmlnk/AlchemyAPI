using Alchemy.Domain.Models;
using Alchemy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Alchemy.Infrastructure.Repositories
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        private readonly AlchemyDbContext _context;

        public ServiceRepository(AlchemyDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
