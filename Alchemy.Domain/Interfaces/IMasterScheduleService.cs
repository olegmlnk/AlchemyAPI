using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IMasterScheduleService
    {
        Task<MasterSchedule?> GetByIdAsync(Guid id);
        Task<List<MasterSchedule>> GetByMasterIdAsync(Guid masterId);
        Task<(Guid? ScheduleId, string? Error)> CreateSlot(Guid masterId, DateTime slotTime);
        Task<(bool Success, string? Error)> MarkSlotAsBooked(Guid id);
        Task<(bool Success, string? Error)> MarkSlotAsAvailable(Guid id);
    }
}
