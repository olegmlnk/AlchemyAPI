using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using AlchemyAPI.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System.Linq;

namespace AlchemyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _service;

        public AppointmentController(IAppointmentService service)
        {
            _service = service;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<List<AppointmentResponse>>> GetAppointments()
        {
            var appointments = await _service.GetAppointment();

            if (appointments == null  || !appointments.Any())
                return NotFound("No appointments found");

            var response = appointments.Select(a => new AppointmentResponse(
                a.Id, 
                a.AppointmentDate.ToString("dd.MM, HH:mm"), 
                a.Description, 
                a.UserId, 
                a.MasterId, 
                a.ServiceId));
            return Ok(response);
        }

        [HttpGet("GetAvailableSlots")]
        public async Task<ActionResult<List<MasterSchedule>>> GetAvailableSlots(long masterId)
        {
            var slots = await _service.GetAvailableSlots(masterId);

            return Ok(slots);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateAppointment([FromBody] AppointmentRequest request)
        {
            var (appointment, error) = Appointment.Create(
                request.AppointmentDate,
                request.Description,
                request.MasterId,
                request.UserId,
                request.ServiceId);

            if (!string.IsNullOrEmpty(error))
                return BadRequest($"Validation error: {error}");

            var appointmentId = await _service.CreateAppointment(appointment);

            if (appointmentId == null)
                return BadRequest("The selected date/time is already booked.");

            return Ok("Appointment booked successfully");
        }

        [HttpPut("Update")]
        public async Task<ActionResult<long>> UpdateAppointment(long id, [FromBody] AppointmentRequest request)
        {
            var appointmentId = await _service.UpdateAppointment(
                id, 
                request.AppointmentDate,
                request.Description, 
                request.MasterId, 
                request.ServiceId, 
                request.UserId);

            return Ok(appointmentId);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<long>> DeleteAppointment(long id)
        {
            return Ok(await _service.DeleteAppointment(id));
        }
    }
}
