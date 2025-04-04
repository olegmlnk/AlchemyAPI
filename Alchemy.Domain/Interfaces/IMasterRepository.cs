using Alchemy.Domain.Models;

namespace Alchemy.Domain.Repositories
{
    public interface IMasterRepository
    {
        Task<long> CreateMaster(Master master);
        Task<long> DeleteMaster(long id);
        Task<long> GetMasterById(long id);
        Task<List<Master>> GetMasters();
        Task<long> UpdateMaster(long id, string name, string expeirence, string description);
    }
}