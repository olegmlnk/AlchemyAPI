using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using Alchemy.Infrastructure.Entities;
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

        public async Task<MasterSchedule?> GetByIdAsync(long id)
        {
            var entity = await _context.MasterSchedules.FindAsync(id);
            return entity == null ? null : MapToDomain(entity);
        }

        public async Task<bool> IsSlotAvailableAsync(long id)
        {
            var entity = await _context.MasterSchedules.FindAsync(id);
            return entity != null && !entity.IsBooked;
        }

        public async Task MarkSlotAsBookedAsync(long id)
        {
            var entity = await _context.MasterSchedules.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Schedule slot not found");

            entity.IsBooked = true;
            _context.MasterSchedules.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task MarkSlotAsAvailableAsync(long id)
        {
            var entity = await _context.MasterSchedules.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Schedule slot not found");

            entity.IsBooked = false;
            _context.MasterSchedules.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<MasterSchedule>> GetAllAsync()
        {
            var entities = await _context.MasterSchedules
                .AsNoTracking()
                .ToListAsync();

            return entities.Select(MapToDomain).ToList();
        }

        public async Task<List<MasterSchedule>> GetByMasterIdAsync(long masterId)
        {
            var entities = await _context.MasterSchedules
                .Where(ms => ms.MasterId == masterId)
                .AsNoTracking()
                .ToListAsync();

            return entities.Select(MapToDomain).ToList();
        }

        private MasterSchedule MapToDomain(MasterScheduleEntity entity)
        {
            return new MasterSchedule
            {
                Id = entity.Id,
                MasterId = entity.MasterId,
                SlotTime = entity.SlotTime,
                IsBooked = entity.IsBooked
            };
        }

        public async Task<bool> UpdateAsync(MasterSchedule schedule)
        {
            var entity = await _context.MasterSchedules.FindAsync(schedule.Id);
            if (entity == null)
                return false;

            entity.IsBooked = schedule.IsBooked;
            entity.SlotTime = schedule.SlotTime;
            entity.MasterId = schedule.MasterId;

            _context.MasterSchedules.Update(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
