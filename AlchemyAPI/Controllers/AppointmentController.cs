using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using AlchemyAPI.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AlchemyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _service;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(IAppointmentService service, ILogger<AppointmentController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<List<AppointmentResponse>>> GetAppointments()
        {
            _logger.LogInformation("Getting all appointments...");

            var appointments = await _service.GetAppointment();

            if (appointments == null || !appointments.Any())
            {
                _logger.LogWarning("No appointments found");
                return NotFound("No appointments found");
            }

            var response = appointments.Select(a => new AppointmentResponse(
                a.Id, 
                a.AppointmentDate.ToString("dd.MM, HH:mm"), 
                a.Description, 
                a.UserId, 
                a.MasterId, 
                a.ServiceId));

            _logger.LogInformation($"Found {appointments.Count} appointments");
            return Ok(response);
        }

        [HttpGet("GetAvailableSlots")]
        public async Task<ActionResult<List<MasterSchedule>>> GetAvailableSlots(long masterId)
        {
            _logger.LogInformation($"Getting available slots for master with id {masterId}...");
            var slots = await _service.GetAvailableSlots(masterId);

            if(slots == null || !slots.Any())
            {
                _logger.LogWarning($"No available slots found for master with id {masterId}");
                return NotFound();
            }

            _logger.LogInformation($"Found {slots.Count} available slots for master with id {masterId}");
            return Ok(slots);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateAppointment([FromBody] AppointmentRequest request)
        {
            _logger.LogInformation("Creating appointment...");

            var (appointment, error) = Appointment.Create(
                request.AppointmentDate,
                request.Description,
                request.MasterId,
                request.UserId,
                request.ServiceId);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest($"Validation error: {error}");
            }

            var appointmentId = await _service.CreateAppointment(appointment);

            if (appointmentId == null)
                return BadRequest("The selected date/time is already booked.");

            return Ok("Appointment booked successfully");
        }

        [HttpPut("Update")]
        public async Task<ActionResult<long>> UpdateAppointment(long id, [FromBody] AppointmentRequest request)
        {
            _logger.LogInformation($"Updating appointment with id {id}...");
            var appointmentId = await _service.UpdateAppointment(
                id, 
                request.AppointmentDate,
                request.Description, 
                request.MasterId, 
                request.ServiceId, 
                request.UserId);

            _logger.LogInformation($"Appointment with id {id} has been updated!");
            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<long>> DeleteAppointment(long id)
        {
            _logger.LogInformation($"Deleting appointment with id {id}...");
            var result = await _service.DeleteAppointment(id);

            _logger.LogInformation($"Appointment with id {id} has been deleted!");
            return Ok();
        }
    }
}
