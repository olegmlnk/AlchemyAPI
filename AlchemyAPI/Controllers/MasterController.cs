using Alchemy.Domain.Models;
using Alchemy.Domain.Services;
using AlchemyAPI.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlchemyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class MasterController : ControllerBase
    {
        private readonly IMasterService _masterService;
        private readonly ILogger<MasterController> _logger;

        public MasterController(IMasterService masterService, ILogger<MasterController> logger)
        {
            _masterService = masterService;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<MasterRepsonse>>> GetMasters()
        { 
            _logger.LogInformation("Getting all masters...");
            var masters = await _masterService.GetMasters();

            var response = masters.Select(m => new MasterRepsonse(m.Id, m.Name, m.Experience, m.Description));

            _logger.LogInformation($"Found {masters.Count} masters");
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateMaster([FromBody] MasterRequest request)
        {
            _logger.LogInformation("Creating a new master...");

            var (master, error) = Master.Create(
                request.Name,
                request.Expeirence,
                request.Description,
                new List<Appointment>());

            if (!string.IsNullOrEmpty(error))
            {
                _logger.LogError($"Error creating master: {error}");
                return BadRequest();
            }

            var masterId = await _masterService.CreateMaster(master);

            _logger.LogInformation("Master has been created successfully");
            return CreatedAtAction(nameof(GetMasters), new { id = masterId }, masterId);
        }

        [HttpPut("Update/{id:}")]
        public async Task<ActionResult<long>> UpdateMaster(long id, [FromBody] MasterRequest request)
        {
            var masterId = await _masterService.UpdateMaster(
                id,
                request.Name, 
                request.Expeirence, 
                request.Description);

            _logger.LogInformation($"Master with id {id} has been updated successfully");
            return Ok(masterId);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<long>> DeleteMaster(long id)
        {
            _logger.LogInformation($"Deleting master with id {id}...");
            var result = await _masterService.DeleteMaster(id);

            _logger.LogInformation($"Master with id {id} has been deleted!");
            return Ok();
        }
    }
}
