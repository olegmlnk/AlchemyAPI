using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Alchemy.Infrastructure.Repositories
{
    public class MasterScheduleRepository : IMasterScheduleRepository
    {
        private readonly AlchemyDbContext _context;
        private readonly IMapper _mapper;

        public MasterScheduleRepository(AlchemyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MasterSchedule?> GetMasterScheduleById(long id)
        {
            var entity = await _context.MasterSchedules
                .AsNoTracking()
                .Include(ms => ms.Master)
                .Include(ms => ms.Appointment)
                .FirstOrDefaultAsync(ms => ms.Id == id);

            if (entity == null)
                throw new KeyNotFoundException("Schedule not found.");

            return _mapper.Map<MasterSchedule>(entity);
        }
        

        public async Task<List<MasterSchedule>> GetAllMasterSchedules()
        {
            var entities = await _context.MasterSchedules
                .AsNoTracking()
                .Include(ms => ms.Master)
                .ToListAsync();

            return _mapper.Map<List<MasterSchedule>>(entities);
        }
        public async Task<List<MasterSchedule>> GetMasterScheduleByMasterId(long masterId)
        {
            var entities = await _context.MasterSchedules
                .AsNoTracking()
                .Where(ms => ms.MasterId == masterId)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<MasterSchedule>>(entities);
        }
        

        public async Task<bool> UpdateMasterSchedule(MasterSchedule schedule)
        {
            var entityToUpdate = await _context.MasterSchedules.FindAsync(schedule.Id);
            
            if (entityToUpdate == null)
                return false;

            _mapper.Map(schedule, entityToUpdate);
            _context.MasterSchedules.Update(entityToUpdate);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<long> CreateMasterSchedule(MasterSchedule schedule)
        {
            var entityToCreate = _mapper.Map<MasterSchedule>(schedule);

            await _context.MasterSchedules.AddAsync(entityToCreate);
            await _context.SaveChangesAsync();
            return entityToCreate.Id;
        }
    }
}