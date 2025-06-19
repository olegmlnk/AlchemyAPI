using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;

namespace Alchemy.Application.Services
{
    public class MasterScheduleService : IMasterScheduleService
    {
        private readonly IMasterScheduleRepository _repository;
        private readonly IMasterRepository _masterRepository;

        public MasterScheduleService(IMasterScheduleRepository repository, IMasterRepository masterRepository)
        {
            _repository = repository;
            _masterRepository = masterRepository;
        }


        public Task<MasterSchedule?> GetByIdAsync(long id)
        {
            return _repository.GetMasterScheduleById(id);
        }

        public Task<List<MasterSchedule>> GetByMasterIdAsync(long masterId)
        {
            return _repository.GetMasterScheduleByMasterId(masterId);
        }

        public async Task<(long? ScheduleId, string? Error)> CreateSlot(long masterId, DateTime slotTime)
        {
            var master = await _masterRepository.GetMasterById(masterId);
            
            if (master == null)
                return (null, "Master not found.");

            var (schedule, error) = MasterSchedule.Create(masterId, slotTime, master);

            if (error != null)
                return (null, error);

            var createdId = await _repository.CreateMasterSchedule(schedule!);
            return (createdId, null);
        }

        public async Task<(bool Success, string? Error)> MarkSlotAsBooked(long id)
        {
            var schedule = await _repository.GetMasterScheduleById(id);

            if (schedule == null)
                return (false, "Slot not found.");

            var (isSuccess, error) = schedule.TryBook(null!);

            if (!isSuccess)
                return (false, null);

            var wasUpdated = await _repository.UpdateMasterSchedule(schedule);

            if (!wasUpdated)
                return (false, "Failed to update slot status");

            return (true, null);
        }

        public async Task<(bool Success, string? Error)> MarkSlotAsAvailable(long id)
        {
            var schedule = await _repository.GetMasterScheduleById(id);

            if (schedule == null)
                return (false, "Slot not found.");

            if (!schedule.IsBooked)
                return (false, "Slot is already available.");

            schedule.TryFreeSlot();
            var wasUpdated = await _repository.UpdateMasterSchedule(schedule);

            if (!wasUpdated)
                return (false, "Failed to update slot status.");

            return (true, null);
        }
    }
}
