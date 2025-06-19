using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IServicesService
    {
        Task<Service?> GetServiceById(long id);
        Task<List<Service>> GetServices();
        Task<(long? ServiceId, string? Error)> CreateService(string title, string description, double price, TimeSpan duration);
        Task<(bool Success, string? Error)> UpdateService(long id, string title, string description, double price, TimeSpan duration);
        Task<bool> DeleteService(long id);
    }
}