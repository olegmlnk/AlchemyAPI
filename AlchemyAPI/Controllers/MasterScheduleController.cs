using Alchemy.Application.Services;
using Alchemy.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Alchemy.Domain.Interfaces;
using AlchemyAPI.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AlchemyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MasterScheduleController : ControllerBase
    {
        private readonly IMasterScheduleService _scheduleService;
        private readonly ILogger<MasterScheduleController> _logger;

        public MasterScheduleController(IMasterScheduleService scheduleService, ILogger<MasterScheduleController> logger)
        {
            _scheduleService = scheduleService;
            _logger = logger;
        }

        [HttpGet("GetByMasterId")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByMasterId(long masterId)
        {
            var slots = await _scheduleService.GetByMasterIdAsync(masterId);

            var response = slots.Select(s => new MasterScheduleResponse(
                s.Id,
                s.MasterId,
                s.Master.Name,
                s.SlotTime,
                s.IsBooked));

            return Ok(response);
        }

        [HttpPost("CreateSlot")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSlot([FromBody] CreateSlotRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Admin trying to create a new schedule slot for MasterId: {MasterId}", request.MasterId);

            var (scheduleId, error) = await _scheduleService.CreateSlot(request.MasterId, request.SlotTime);

            if (error != null)
            {
                return BadRequest(new { Error = error });
            }

            return Ok(new { Id = scheduleId });
        }

        [HttpPost("MaskSlotAsAvailable")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MarkSlotAsAvailable(long id)
        {
            var (success, error) = await _scheduleService.MarkSlotAsAvailable(id);
            if (!success)
            {
                if (error != null && error.Contains("not found"))
                    return NotFound();
                if (error != null && error.Contains("already available"))
                    return Conflict(new { Error = error });

                return BadRequest(new { Error = error ?? "Failed to free the slot." });
            }

            _logger.LogInformation($"Slot with ID {id} was manually freed by an admin.");
            return NoContent();
        }
    }
}