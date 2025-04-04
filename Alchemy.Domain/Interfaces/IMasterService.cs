using Alchemy.Domain.Models;

namespace Alchemy.Domain.Services
{
    public interface IMasterService
    {
        Task<long> CreateMaster(Master master);
        Task<long> DeleteMaster(long id);
        Task<long> GetMasterById(long id);
        Task<List<Master>> GetMasters();
        Task<long> UpdateMaster(long id, string name, string expeirence, string description);
    }
}