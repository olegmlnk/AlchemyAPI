using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IMasterService
    {
        Task<Master?> GetMasterById(long id);
        Task<List<Master>> GetAllMasters();
        Task<(long? MasterId, string? Error)> CreateMaster(string name, string experience, string description);
        Task<(bool Success, string? Error)> UpdateMaster(long id, string name, string experience, string description);
        Task<bool> DeleteMaster(long id);
    }
}