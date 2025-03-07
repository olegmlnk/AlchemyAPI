using Alchemy.Domain.Models;
using Alchemy.Domain.Services;
using AlchemyAPI.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlchemyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IMasterService _masterService;

        public MasterController(IMasterService masterService)
        {
            _masterService = masterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<MasterRepsonse>>> GetMasters()
        {
            var masters = await _masterService.GetMasters();

            var response = masters.Select(m => new MasterRepsonse(m.Id, m.Name, m.Expeirence, m.Description));

            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateMaster([FromBody] MasterRequest request)
        {
            var (master, error) = Master.Create(
                Guid.NewGuid(),
                request.Name,
                request.Expeirence,
                request.Description);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var masterId = await _masterService.CreateMaster(master);

            return Ok(masterId);
        }

        [HttpPut("Update{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateMaster(Guid id, [FromBody] MasterRequest request)
        {
            var masterId = await _masterService.UpdateMaster(id, request.Name, request.Expeirence, request.Description);

            return Ok(masterId);
        }

        [HttpDelete("Delete{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteMaster(Guid id)
        {
            return Ok(await _masterService.DeleteMaster(id));
        }
    }
}
