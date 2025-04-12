using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;

namespace Alchemy.Application.Services
{
    public class MasterScheduleService : IMasterScheduleService
    {
        private readonly IMasterScheduleRepository _repository;

        public MasterScheduleService(IMasterScheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<MasterSchedule>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MasterSchedule?> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<MasterSchedule>> GetByMasterIdAsync(long masterId)
        {
            return await _repository.GetByMasterIdAsync(masterId);
        }

        public async Task<bool> IsSlotAvailableAsync(long id)
        {
            var slot = await _repository.GetByIdAsync(id);
            if (slot == null)
                throw new KeyNotFoundException("Schedule slot not found.");

            return !slot.IsBooked;
        }

        public async Task MarkSlotAsBookedAsync(long id)
        {
            var slot = await _repository.GetByIdAsync(id);
            if (slot == null)
                throw new KeyNotFoundException("Schedule slot not found.");

            if (slot.IsBooked)
                throw new InvalidOperationException("Slot already booked.");

            slot.MarkAsBooked();
            await _repository.UpdateAsync(slot);
        }

        public async Task MarkSlotAsAvailableAsync(long id)
        {
            var slot = await _repository.GetByIdAsync(id);
            if (slot == null)
                throw new KeyNotFoundException("Schedule slot not found.");

            if (!slot.IsBooked)
                throw new InvalidOperationException("Slot already available.");

            slot.MarkAsAvailable();
            await _repository.UpdateAsync(slot);
        }
    }
}
