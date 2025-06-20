using System.Security.Claims;
using Alchemy.Domain.Interfaces;
using AlchemyAPI.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlchemyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(IAppointmentService appointmentService, ILogger<AppointmentController> logger)
        {
            _appointmentService = appointmentService;
            _logger = logger;
        }

        [HttpGet("GetAllAppointments")]
        public async Task<IActionResult> GetAppointments()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (User.IsInRole("Admin"))
            {
                var allAppointments = await _appointmentService.GetAllAppointments();
                var adminResponse = allAppointments.Select(a => new AppointmentResponse(
                    a.Id, a.ScheduleSlotId, a.Description, a.UserId, a.MasterId, a.ServiceId));
                return Ok(adminResponse);
            }

            var userAppointments = await _appointmentService.GetAppointmentsByUserId(currentUserId);
            var userResponse = userAppointments.Select(a => new AppointmentResponse(
                a.Id, a.ScheduleSlotId, a.Description, a.UserId, a.MasterId, a.ServiceId));

            return Ok(userResponse);
        }

        [HttpGet("GetAppointmentById")]
        public async Task<IActionResult> GetAppointmentById(long id)
        {
            var appointment = await _appointmentService.GetAppointmentById(id);

            if (appointment == null)
                return NotFound();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (appointment.UserId != currentUserId && !User.IsInRole("Admin"))
            {
                _logger.LogWarning(
                    $"User {currentUserId} attempted to access appointment {appointment} without permission.",
                    currentUserId, id);
                return Forbid();
            }

            var response = new AppointmentResponse(
                appointment.Id,
                appointment.ScheduleSlotId,
                appointment.Description,
                appointment.UserId,
                appointment.MasterId,
                appointment.ServiceId);

            return Ok(response);
        }

        [HttpGet("GetByUserId")]
        public async Task<IActionResult> GetAppointmentsByUserId(string id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("GetByMasterId")]
        public async Task<IActionResult> GetAppointmentsByMasterId(long id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("CreateAppointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(currentUserId))
                return Unauthorized("User not found.");

            var (createdAppointmentId, creationError) = await _appointmentService.CreateAppointment(
                request.ScheduleSlotId,
                request.Description,
                request.MasterId,
                request.ServiceId,
                currentUserId);

            if (creationError != null)
            {
                if (creationError.Contains("not found")) return NotFound(new { Error = creationError });
                if (creationError.Contains("booked") || creationError.Contains("in advance"))
                    return Conflict(new { Error = creationError });
                return BadRequest(new { Error = creationError });
            }

            var response = new AppointmentResponse(
                createdAppointmentId!.Value,
                request.ScheduleSlotId,
                request.Description,
                currentUserId,
                request.MasterId,
                request.ServiceId);

            return CreatedAtAction(nameof(GetAppointmentById), new { id = createdAppointmentId.Value }, response);
        }

        [HttpPut("UpdateAppointment")]
        public async Task<IActionResult> UpdateAppointment(long id, [FromBody] AppointmentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(currentUserId))
                return Unauthorized();

            var (success, error) = await _appointmentService.UpdateAppointment(
                id,
                request.Description,
                currentUserId);

            if (!success)
            {
                if (error != null && error.Contains("not found"))
                {
                    return NotFound(new { Error = error });
                }

                if (error != null && error.Contains("permission"))
                {
                    _logger.LogWarning($"Forbidden attempt: User {currentUserId} tried to update appointment {id}");
                    return Forbid();
                }

                return BadRequest(new { Error = error ?? "Failed to update the appointment" });
            }
                _logger.LogInformation($"Appointment {id} was updated by User {currentUserId}");
                return NoContent();
        }
    

    [HttpDelete("CancelAppointment")]
        public async Task<IActionResult> CancelAppointment(long id)
        {
            var appointment = await _appointmentService.GetAppointmentById(id);

            if (appointment == null)
                return NotFound(new { Error = "Appointment not found." });

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isUserAdmin = User.IsInRole("Admin");

            if (appointment.UserId != currentUserId && !isUserAdmin)
            {
                _logger.LogWarning(
                    $"Forbidden access attempt: User {currentUserId} tried to cancel appointment {id} owned by {appointment.UserId}");
                return Forbid();
            }

            var (success, error) = await _appointmentService.CancelAppointment(id, currentUserId, isUserAdmin);

            if (!success)
                return BadRequest(new { Error = error ?? "Failed to cancel the appointment" });

            _logger.LogInformation($"Appointment {id} was successfully canceled by User {currentUserId}");

            return NoContent();
        }

    }
}
