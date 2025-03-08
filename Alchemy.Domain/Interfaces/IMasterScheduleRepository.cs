using Alchemy.Domain.Models;

namespace Alchemy.Domain.Repositories
{
    public interface IMasterScheduleRepository
    {
        Task AddSlot(MasterSchedule slot);
        Task<bool> BookSlot(Guid slotId);
        Task<List<MasterSchedule>> GetAvailableSlots(Guid masterId);
    }
}