using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IServicesService
    {
        Task<Service?> GetServiceById(Guid id);
        Task<List<Service>> GetServices();
        Task<(Guid? ServiceId, string? Error)> CreateService(string title, string description, double price, TimeSpan duration);
        Task<(bool Success, string? Error)> UpdateService(Guid id, string title, string description, double price, TimeSpan duration);
        Task<bool> DeleteService(Guid id);
    }
}