using Alchemy.Domain.Models;

namespace Alchemy.Domain.Repositories
{
    public interface IServiceRepository
    {
        Task<Guid> CreateService(Service service);
        Task<Guid> DeleteService(Guid id);
        Task<List<Service>> GetServices();
        Task<Guid> GetServiceById(Guid id);
        Task<Guid> UpdateService(Guid id, string title, string description, decimal price, int duration);
    }
}