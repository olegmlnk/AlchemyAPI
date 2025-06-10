using Alchemy.Domain.Models;
using Alchemy.Domain.Repositories;
using Alchemy.Infrastructure.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Alchemy.Infrastructure.Repositories
{
    public class MasterRepository : IMasterRepository
    {
        private readonly AlchemyDbContext _context;
        private readonly IMapper _mapper;

        public MasterRepository(AlchemyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Master>> GetAllMasters()
        {
            var entities = await _context.Masters
                .AsNoTracking()
                .ToListAsync();
            return _mapper.Map<List<Master>>(entities);
        }


        public async Task<Master?> GetMasterById(long id)
        {
            var entity = await _context.Masters
                .AsNoTracking()
                .Include(m => m.ScheduleSlots)
                .FirstOrDefaultAsync(m => m.Id == id);

            return _mapper.Map<Master>(entity);
        }

        public async Task<long> CreateMaster(Master master)
        {
            var masterEntity = _mapper.Map<MasterEntity>(master);
            await _context.Masters.AddAsync(masterEntity);
            await _context.SaveChangesAsync();

            return masterEntity.Id;
        }

        public async Task<bool> UpdateMaster(Master master)
        {
            var entityToUpdate = await _context.Masters.FindAsync(master.Id);

            if (entityToUpdate == null)
                return false;

            _mapper.Map(master, entityToUpdate);
            _context.Masters.Update(entityToUpdate);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteMaster(long id)
        {
            var master = await _context.Masters.FindAsync(id);

            if (master == null)
                throw new KeyNotFoundException("Master not found");

            _context.Masters.Remove(master);
            await _context.SaveChangesAsync();

            return await _context.SaveChangesAsync() > 0;
        }

    }
}
