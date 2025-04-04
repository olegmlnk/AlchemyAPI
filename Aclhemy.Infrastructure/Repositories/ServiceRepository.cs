using Alchemy.Domain.Models;
using Alchemy.Domain.Repositories;
using Alchemy.Infrastructure.Entities;
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

            var services = serviceEntities
                .Select(s => Service.Create(s.Id, s.Title, s.Description, s.Price, s.Duration).Service)
                .ToList();

            return services;
        }

        public async Task<long> GetServiceById(long id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
                throw new KeyNotFoundException("Service not found");

            return service.Id;
        }

        public async Task<long> CreateService(Service service)
        {
            var serviceEntity = new ServiceEntity
            {
                Id = service.Id,
                Title = service.Title,
                Description = service.Description,
                Price = service.Price,
                Duration = service.Duration
            };

            await _context.Services.AddAsync(serviceEntity);
            await _context.SaveChangesAsync();

            return service.Id;
        }

        public async Task<long> UpdateService(long id, string title, string description, decimal price, int duration)
        {
            await _context.Services
                .Where(s => s.Id == id)
                .ExecuteUpdateAsync(x => x
                .SetProperty(s => s.Title, s => title)
                .SetProperty(s => s.Description, s => description)
                .SetProperty(s => s.Price, s => price)
                .SetProperty(s => s.Duration, s => duration)
                );
            return id;
        }

        public async Task<long> DeleteService(long id)
        {
            await _context.Services
                .Where(s => s.Id == id)
                .ExecuteDeleteAsync();
            return id;
        }
    }
}
