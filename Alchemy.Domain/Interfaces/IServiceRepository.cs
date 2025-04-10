using Alchemy.Domain.Models;

namespace Alchemy.Domain.Repositories
{
    public interface IServiceRepository
    {
        Task<long> CreateService(Service service);
        Task<long> DeleteService(long id);
        Task<List<Service>> GetServices();
        Task<long> GetServiceById(long id);
        Task<long> UpdateService(long id, string title, string description, double price, double duration);
    }
}