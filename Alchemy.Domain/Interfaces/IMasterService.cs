using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IMasterService
    {
        Task<Master?> GetMasterById(Guid id);
        Task<List<Master>> GetAllMasters();
        Task<(Guid? MasterId, string? Error)> CreateMaster(string name, string experience, string description);
        Task<(bool Success, string? Error)> UpdateMaster(Guid id, string name, string experience, string description);
        Task<bool> DeleteMaster(Guid id);
    }
}