using Alchemy.Domain.Models;
using Alchemy.Domain.Repositories;
using Alchemy.Domain.Services;

namespace Alchemy.Application.Services
{
    public class MasterService : IMasterService
    {
        private readonly IMasterRepository _masterRepository;

        public MasterService(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }

        public async Task<List<Master>> GetMasters()
        {
            return await _masterRepository.GetMasters();
        }

        public async Task<Guid> GetMasterById(Guid id)
        {
            return await _masterRepository.GetMasterById(id);
        }

        public async Task<Guid> CreateMaster(Master master)
        {
            return await _masterRepository.CreateMaster(master);
        }

        public async Task<Guid> UpdateMaster(Guid id, string name, string expeirence, string description)
        {
            return await _masterRepository.UpdateMaster(id, name, expeirence, description);
        }

        public async Task<Guid> DeleteMaster(Guid id)
        {
            return await _masterRepository.DeleteMaster(id);
        }
    }
}
