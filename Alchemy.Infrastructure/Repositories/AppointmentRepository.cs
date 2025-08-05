using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;

namespace Alchemy.Infrastructure.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly AlchemyDbContext _context;

        public AppointmentRepository(AlchemyDbContext context) : base(context)
        {
            _context = context;
        }
        
        //TODO The most important repo, think about the methods you need here
    }
}
