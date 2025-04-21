using Alchemy.Domain.Models;
using Alchemy.Domain.Repositories;
using Alchemy.Domain.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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

        public async Task<long> GetMasterById(long id)
        {
            return await _masterRepository.GetMasterById(id);
        }

        public async Task<long> CreateMaster(Master master)
        {
            return await _masterRepository.CreateMaster(master);
        }

        public async Task<long> UpdateMaster(long id, string name, string expeirence, string description)
        {
            return await _masterRepository.UpdateMaster(id, name, expeirence, description);
        }

        public async Task<long> DeleteMaster(long id)
        {
            return await _masterRepository.DeleteMaster(id);
        }
    }
}
