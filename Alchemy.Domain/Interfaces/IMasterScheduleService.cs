using Alchemy.Domain.Models;

namespace Alchemy.Application.Services
{
    public interface IMasterScheduleService
    {
        Task<List<MasterSchedule>> GetAllAsync();
        Task<MasterSchedule?> GetByIdAsync(long id);
        Task<List<MasterSchedule>> GetByMasterIdAsync(long masterId);
        Task<bool> IsSlotAvailableAsync(long id);
        Task MarkSlotAsBookedAsync(long id);
        Task MarkSlotAsAvailableAsync(long id);
    }
}
