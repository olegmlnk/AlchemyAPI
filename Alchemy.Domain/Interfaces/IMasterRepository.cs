using Alchemy.Domain.Models;

namespace Alchemy.Domain.Repositories
{
    public interface IMasterRepository
    {
        Task<Master?> GetMasterById(long id);
        Task<List<Master>> GetAllMasters();
        Task<long> CreateMaster(Master master);
        Task<bool> UpdateMaster(Master master);
        Task<bool> DeleteMaster(long id);
    }
}