using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;

namespace Alchemy.Infrastructure.Repositories
{
    public class MasterScheduleRepository : GenericRepository<MasterSchedule>, IMasterScheduleRepository
    {
        private readonly AlchemyDbContext _context;

        public MasterScheduleRepository(AlchemyDbContext context) : base(context)
        {
            _context = context;
        }
        
    }
}