using Alchemy.Domain.Models;
using Alchemy.Domain.Interfaces;
using Alchemy.Infrastructure.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Alchemy.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AlchemyDbContext _context;
        private readonly IMapper _mapper;

        public ServiceRepository(AlchemyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Service>> GetServices()
        {
            var serviceEntities = await _context.Services
                .AsNoTracking()
                .ToListAsync();
            
            return _mapper.Map<List<Service>>(serviceEntities);
        }

        public async Task<Service?> GetServiceById(long id)
        {
            var service = await _context.Services
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
            
            if (service == null)
                throw new KeyNotFoundException("Service not found");

            return _mapper.Map<Service>(service);
        }

        public async Task<long> CreateService(Service service)
        {
            var serviceEntity = _mapper.Map<ServiceEntity>(service);
            await _context.Services.AddAsync(serviceEntity);
            await _context.SaveChangesAsync();
            return serviceEntity.Id;
        }

        public async Task<bool> UpdateService(Service service)
        {
            var serviceToUpdate = await _context.Services.FindAsync(service.Id);

            if (serviceToUpdate == null)
                return false;

            _mapper.Map(service, serviceToUpdate);
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
