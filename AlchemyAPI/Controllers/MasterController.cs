using Alchemy.Domain.Models;
using Alchemy.Domain.Interfaces;
using AlchemyAPI.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlchemyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IMasterService _masterService;
        private readonly ILogger<MasterController> _logger;

        public MasterController(IMasterService masterService, ILogger<MasterController> logger)
        {
            _masterService = masterService;
            _logger = logger;
        }

        [HttpGet("GetMasters")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMasters()
        {
            _logger.LogInformation("Getting all masters...");
            var masters = await _masterService.GetAllMasters();

            var response = masters.Select(m => new MasterRepsonse(
                m.Id,
                m.Name,
                m.Experience,
                m.Description
                ));

            _logger.LogInformation($"Found {masters.Count} masters.");
            return Ok(response);
        }

        [HttpGet("GetMasterById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMasterById(long id)
        {
            var master = await _masterService.GetMasterById(id);

            if (master == null)
                return NotFound();

            var response = new MasterRepsonse(
                master.Id,
                master.Name, 
                master.Experience,
                master.Description
                );

            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateMaster([FromBody] MasterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Attempting to create a new master...");

            var (masterId, error) = await _masterService.CreateMaster(
                request.Name,
                request.Experience,
                request.Description);

            if (error != null)
            {
                _logger.LogWarning($"Failed to create master: {error}");
                return BadRequest(new { Error = error });
            }

            _logger.LogInformation($"Master with ID {masterId} has been created successfully");
            return CreatedAtAction(nameof(GetMasterById), new { id = masterId });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateMaster(long id, [FromBody] MasterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (success, error) = await _masterService.UpdateMaster(
                id,
                request.Name,
                request.Experience,
                request.Description);

            if (error != null)
                return NotFound(new { Error = error });

            if (!success)
                return BadRequest(new { Error = "Failed to update master" });

            _logger.LogInformation($"Master with ID {id} has been updated successfully.");
            return NoContent();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteMaster(long id)
        {
            _logger.LogInformation($"Deleting master with ID {id}");
            var success = await _masterService.DeleteMaster(id);

            if (!success)
            {
                _logger.LogWarning($"Master with ID {id} not found for deletion");
                return NotFound(new { Error = "Master not found" });
            }

            _logger.LogInformation($"Master with ID {id} has been deleted!");
            return NoContent();
        }
    }
}
