using Alchemy.Application.Services;
using Alchemy.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alchemy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MasterScheduleController : ControllerBase
    {
        private readonly IMasterScheduleService _scheduleService;

        public MasterScheduleController(IMasterScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MasterSchedule>>> GetAll()
        {
            var schedules = await _scheduleService.GetAllAsync();
            return Ok(schedules);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MasterSchedule>> GetById(long id)
        {
            var schedule = await _scheduleService.GetByIdAsync(id);
            if (schedule == null)
                return NotFound();

            return Ok(schedule);
        }

        [HttpGet("master/{masterId}")]
        public async Task<ActionResult<List<MasterSchedule>>> GetByMasterId(long masterId)
        {
            var slots = await _scheduleService.GetByMasterIdAsync(masterId);
            return Ok(slots);
        }

        [HttpGet("{id}/available")]
        public async Task<ActionResult<bool>> IsSlotAvailable(long id)
        {
            var isAvailable = await _scheduleService.IsSlotAvailableAsync(id);
            return Ok(isAvailable);
        }

        [HttpPut("{id}/book")]
        public async Task<IActionResult> MarkAsBooked(long id)
        {
            try
            {
                await _scheduleService.MarkSlotAsBookedAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}/free")]
        public async Task<IActionResult> MarkAsAvailable(long id)
        {
            try
            {
                await _scheduleService.MarkSlotAsAvailableAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
