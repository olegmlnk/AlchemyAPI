using Alchemy.Domain.Models;
using Alchemy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Alchemy.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AlchemyDbContext _context;

        public ServiceRepository(AlchemyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Service>> GetServices()
        {
            var serviceEntities = await _context.Services
                .AsNoTracking()
                .ToListAsync();
            
            return serviceEntities;
        }

        public async Task<Service?> GetServiceById(long id)
        {
            var service = await _context.Services
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
            
            if (service == null)
                throw new KeyNotFoundException("Service not found");

            return service;
        }

        public async Task<long> CreateService(Service service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return service.Id;
        }

        public async Task<bool> UpdateService(Service service)
        {
            var serviceToUpdate = await _context.Services.FindAsync(service.Id);

            if (serviceToUpdate == null)
                return false;
            
            _context.Services.Update(serviceToUpdate);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteService(long id)
        {
            var serviceToDelete = await _context.Services.FindAsync(id);
            if (serviceToDelete == null)
                return false;

            _context.Services.Remove(serviceToDelete);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
