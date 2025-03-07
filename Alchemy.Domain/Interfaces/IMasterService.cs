using Alchemy.Domain.Models;

namespace Alchemy.Domain.Services
{
    public interface IMasterService
    {
        Task<Guid> CreateMaster(Master master);
        Task<Guid> DeleteMaster(Guid id);
        Task<Guid> GetMasterById(Guid id);
        Task<List<Master>> GetMasters();
        Task<Guid> UpdateMaster(Guid id, string name, string expeirence, string description);
    }
}