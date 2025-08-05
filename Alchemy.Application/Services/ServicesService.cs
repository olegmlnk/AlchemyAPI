using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;

namespace Alchemy.Application.Services
{
    public class ServicesService : IServicesService
    {
        private readonly IGenericRepository<Service> _serviceRepository;

        public ServicesService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }


        public Task<Service?> GetServiceById(Guid id)
        {
            return _serviceRepository.GetAsync(id);
        }

        public Task<List<Service>> GetServices()
        {
            return _serviceRepository.GetAllAsync();
        }

        public async Task<(Guid? ServiceId, string? Error)> CreateService(string title, string description, double price, TimeSpan duration)
        {
            var (service, error) = Service.Create(title, description, price, duration);

            if (error != null)
                return (null, error);

            var createdId = await _serviceRepository.AddAsync(service);
            return (createdId, null);
        }

        public async Task<(bool Success, string? Error)> UpdateService(Guid id, string title, string description, double price, TimeSpan duration)
        {
            var service = await _serviceRepository.GetAsync(id);

            if (service == null)
                return (false, "Service not found!");

            var (isUpdateSuccessful, error) = service.UpdateDetails(title, description, price, duration);

            if (!isUpdateSuccessful)
                return (false, error);

            var success = await _serviceRepository.UpdateService(service);
            return (success, success ? null : "Service hasn't been updated.");
        }

        public Task<bool> DeleteService(Guid id)
        {
            return _serviceRepository.DeleteAsync(id);
        }
    }
}
