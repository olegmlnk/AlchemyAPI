using Alchemy.Domain.Models;
using Alchemy.Domain.Repositories;
using Alchemy.Infrastructure.Entities;
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

        public async Task<List<Master>> GetMasters()
        {
            var masterEntities = await _context.Masters
                .AsNoTracking()
                .Include(m => m.Appointments) 
                .ToListAsync();

            var masters = masterEntities
                .Select(m => Master.Create(
                    m.Name,
                    m.Expeirence,
                    m.Description,
                    m.Appointments
                        .Select(a => Appointment.Create(
                            a.AppointmentDate,
                            a.Description, 
                            a.MasterId, 
                            a.ServiceId, 
                            a.UserId).Appointment)
                        .ToList()
                ).master)
                .ToList();

            return masters;
        }


        public async Task<long> GetMasterById(long id)
        {
            var master = await _context.Masters.FindAsync(id);
            if (master == null)
                throw new KeyNotFoundException("Master not found");
            return master.Id;
        }

        public async Task<long> CreateMaster(Master master)
        {
            var masterEntity = new MasterEntity
            {
                Id = master.Id,
                Name = master.Name,
                Expeirence = master.Expeirence,
                Description = master.Description
            };

            await _context.Masters.AddAsync(masterEntity);
            await _context.SaveChangesAsync();

            return master.Id;
        }

        public async Task<long> UpdateMaster(long id, string name, string expeirence, string description)
        {
            await _context.Masters.Where(m => m.Id == id)
                .ExecuteUpdateAsync(x => x
                .SetProperty(m => m.Name, m => name)
                .SetProperty(m => m.Expeirence, m => expeirence)
                .SetProperty(m => m.Description, m => description)
                );

            return id;
        }

        public async Task<long> DeleteMaster(long id)
        {
            var master = await _context.Masters.FindAsync(id);

            if (master == null)
                throw new KeyNotFoundException("Master not found");

            _context.Masters.Remove(master);
            await _context.SaveChangesAsync();

            return master.Id;
        }

    }
}
