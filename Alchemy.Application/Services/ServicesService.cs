using Alchemy.Domain.Models;
using Alchemy.Domain.Repositories;
using Alchemy.Domain.Services;

namespace Alchemy.Application.Services
{
    public class ServicesService : IServicesService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServicesService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<List<Service>> GetServices()
        {
            return await _serviceRepository.GetServices();
        }

        public async Task<Guid> GetServiceById(Guid id)
        {
            return await _serviceRepository.GetServiceById(id);
        }

        public async Task<Guid> CreateService(Service service)
        {
            return await _serviceRepository.CreateService(service);
        }

        public async Task<Guid> UpdateService(Guid id, string title, string description, decimal price, int duration)
        {
            return await _serviceRepository.UpdateService(id, title, description, price, duration);
        }

        public async Task<Guid> DeleteService(Guid id)
        {
            return await _serviceRepository.DeleteService(id);
        }

    }
}
