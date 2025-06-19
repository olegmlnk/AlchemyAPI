using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IMasterScheduleRepository
    {
        Task<MasterSchedule?> GetMasterScheduleById(long id);
        Task<List<MasterSchedule>> GetAllMasterSchedules();
        Task<List<MasterSchedule>> GetMasterScheduleByMasterId(long masterId);
        Task<long> CreateMasterSchedule(MasterSchedule schedule);
        Task<bool> UpdateMasterSchedule(MasterSchedule schedule);
    }
}
