using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IServicesService
    {
        Task<long> CreateService(Service service);
        Task<long> DeleteService(long id);
        Task<long> GetServiceById(long id);
        Task<List<Service>> GetServices();
        Task<long> UpdateService(long id, string title, string description, double price, int duration);
    }
}