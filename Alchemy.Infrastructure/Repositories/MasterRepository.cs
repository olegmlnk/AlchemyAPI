using Alchemy.Domain.Models;
using Alchemy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Alchemy.Infrastructure.Repositories
{
    public class MasterRepository : IMasterRepository
    {
        private readonly AlchemyDbContext _context;

        public MasterRepository(AlchemyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Master>> GetAllMasters()
        {
            var entities = await _context.Masters
                .AsNoTracking()
                .ToListAsync();
            return entities;
        }
 

        public async Task<Master?> GetMasterById(long id)
        {
            var entity = await _context.Masters
                .AsNoTracking()
                .Include(m => m.MasterSchedules)
                .FirstOrDefaultAsync(m => m.Id == id);

            return (entity);
        }

        public async Task<long> CreateMaster(Master master)
        {
            var masterEntity = Master.Create(
                master.Name, master.Experience, master.Description);
            
            await _context.Masters.AddAsync(master);
            await _context.SaveChangesAsync();

            return master.Id;
        }

        public async Task<bool> UpdateMaster(Master master)
        {
            var entityToUpdate = await _context.Masters.FindAsync(master.Id);

            if (entityToUpdate == null)
                return false;

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
