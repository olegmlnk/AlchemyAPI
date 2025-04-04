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

        public async Task<long> GetServiceById(long id)
        {
            return await _serviceRepository.GetServiceById(id);
        }

        public async Task<long> CreateService(Service service)
        {
            return await _serviceRepository.CreateService(service);
        }

        public async Task<long> UpdateService(long id, string title, string description, decimal price, int duration)
        {
            return await _serviceRepository.UpdateService(id, title, description, price, duration);
        }

        public async Task<long> DeleteService(long id)
        {
            return await _serviceRepository.DeleteService(id);
        }

    }
}
