using Alchemy.Domain.Models;
using Alchemy.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Alchemy.Infrastructure.Repositories
{
    public class MasterScheduleRepository : IMasterScheduleRepository
    {
        private readonly AlchemyDbContext _context;

        public MasterScheduleRepository(AlchemyDbContext context)
        {
            _context = context;
        }

        public async Task<List<MasterSchedule>> GetAvailableSlots(Guid masterId)
        {
            return await _context.MasterSchedules
                .Where(x => x.MasterId == masterId && x.IsBooked == false)
                .ToListAsync();
        }

        public async Task<bool> BookSlot(Guid slotId)
        {
            var slot = await _context.MasterSchedules.FindAsync(slotId);

            if (slot == null || slot.IsBooked)
            {
                return false;
            }

            slot.IsBooked = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddSlot(MasterSchedule slot)
        {
            await _context.MasterSchedules.AddAsync(slot);
            await _context.SaveChangesAsync();
        }
    }
}
