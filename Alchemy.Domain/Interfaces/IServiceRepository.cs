using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IServiceRepository
    {
        Task<List<Service>> GetServices();
        Task<Service?> GetServiceById(long id);
        Task<bool> UpdateService(Service service);
        Task<long> CreateService(Service service);
        Task<bool> DeleteService(long id);
    }
}