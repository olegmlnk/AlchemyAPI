using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IMasterScheduleService
    {
        Task<MasterSchedule?> GetByIdAsync(long id);
        Task<List<MasterSchedule>> GetByMasterIdAsync(long masterId);
        Task<(long? ScheduleId, string? Error)> CreateSlot(long masterId, DateTime slotTime);
        Task<(bool Success, string? Error)> MarkSlotAsBooked(long id);
        Task<(bool Success, string? Error)> MarkSlotAsAvailable(long id);
    }
}
