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

        public async Task<List<MasterSchedule>> GetAvailableSlots(long masterId)
        {
            //var now = DateTime.UtcNow;
            //var maxDate = now.AddDays(30);

            //return await _context.MasterSchedules
            //    .Where(slot =>
            //        slot.MasterId == masterId &&
            //        !slot.IsBooked &&
            //        slot.SlotTime >= now &&
            //        slot.SlotTime <= maxDate)
            //    .OrderBy(slot => slot.SlotTime)
            //    .ToListAsync();
            throw new Exception("Not implemented yet");
        }

        public async Task<bool> BookSlot(long slotId)
        {
            var slot = await _context.MasterSchedules.FirstOrDefaultAsync(s => s.Id == slotId);

            if (slot is null || slot.IsBooked)
                return false;

            slot.IsBooked = true;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}
