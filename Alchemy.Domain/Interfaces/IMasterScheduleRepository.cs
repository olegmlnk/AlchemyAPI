using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IMasterScheduleRepository
    {
        Task<MasterSchedule?> GetByIdAsync(long id);
        Task<bool> IsSlotAvailableAsync(long id);
        Task MarkSlotAsBookedAsync(long id);
        Task MarkSlotAsAvailableAsync(long id);
        Task<List<MasterSchedule>> GetAllAsync();
        Task<List<MasterSchedule>> GetByMasterIdAsync(long masterId);
        Task<bool> UpdateAsync(MasterSchedule schedule);

    }
}
