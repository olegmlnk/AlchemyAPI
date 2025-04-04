using Alchemy.Domain.Models;

namespace Alchemy.Domain.Repositories
{
    public interface IMasterScheduleRepository
    {
        Task AddSlot(MasterSchedule slot);
        Task<bool> BookSlot(long slotId);
        Task<List<MasterSchedule>> GetAvailableSlots(long masterId);
    }
}