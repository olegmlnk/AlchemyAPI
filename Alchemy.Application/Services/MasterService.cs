using Alchemy.Domain.Models;
using Alchemy.Domain.Interfaces;

namespace Alchemy.Application.Services
{
    public class MasterService : IMasterService
    {
        private readonly IMasterRepository _masterRepository;

        public MasterService(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }

        public Task<Master?> GetMasterById(long id)
        {
            return _masterRepository.GetMasterById(id);
        }

        public Task<List<Master>> GetAllMasters()
        {
            return _masterRepository.GetAllMasters();
        }

        public async Task<(long? MasterId, string? Error)> CreateMaster(string name, string experience, string description)
        {
            var (master, error) = Master.Create(name, experience, description);

            if (error != null)
                return (null, error);

            var createdId = await _masterRepository.CreateMaster(master!);
            return (createdId, null);
        }

        public async Task<(bool Success, string? Error)> UpdateMaster(long id, string name, string experience, string description)
        {
            var master = await _masterRepository.GetMasterById(id);
            if (master == null) 
                return (false, "Master not found!");

            var (isUpdateSuccessful, error) = master.UpdateDetails(name, experience, description);

            if (!isUpdateSuccessful)
                return (false, error);

            var success = await _masterRepository.UpdateMaster(master);
            return (success, success ? null : "Master hasn't been updated.");
        }

        public Task<bool> DeleteMaster(long id)
        {
            return _masterRepository.DeleteMaster(id);
        }
    }
}
