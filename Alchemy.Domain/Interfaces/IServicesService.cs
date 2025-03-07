using Alchemy.Domain.Models;

namespace Alchemy.Domain.Services
{
    public interface IServicesService
    {
        Task<Guid> CreateService(Service service);
        Task<Guid> DeleteService(Guid id);
        Task<Guid> GetServiceById(Guid id);
        Task<List<Service>> GetServices();
        Task<Guid> UpdateService(Guid id, string title, string description, decimal price, int duration);
    }
}