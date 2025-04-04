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

        public AppointmentController(IAppointmentService service)
        {
            _service = service;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<List<AppointmentResponse>>> GetAppointments()
        {
            var appointments = await _service.GetAppointment();

            var response = appointments.Select(a => new AppointmentResponse(a.Id, a.AppointmentDate, a.Description, a.UserId, a.MasterId, a.ServiceId));
            return Ok(response);
        }

        [HttpGet("GetAvailableSlots/{masterId:guid}")]
        public async Task<ActionResult<List<MasterSchedule>>> GetAvailableSlots(long masterId)
        {
            var slots = await _service.GetAvailableSlots(masterId);

            return Ok(slots);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateAppointment([FromBody] AppointmentRequest request)
        {
            var (appointment, error) = Appointment.Create(
                long.NewGuid(),
                request.AppointmentDate,
                request.Description,
                request.MasterId,
                request.UserId,
                request.ServiceId);

            if(!string.IsNullOrEmpty(error))
            {
                return BadRequest("Chosen date is not available");
            }

            var appointmentId = await _service.CreateAppointment(appointment);
            if (appointmentId == long.Empty)
            {
                return BadRequest("Chosen date is not available");
            }
            return Ok("Appointment booked successfully");
        }

        [HttpPut("Update{id:guid}")]
        public async Task<ActionResult<long>> UpdateAppointment(long id, [FromBody] AppointmentRequest request)
        {
            var appointmentId = await _service.UpdateAppointment(id, request.AppointmentDate, request.Description, request.MasterId, request.ServiceId, request.UserId);

            return Ok(appointmentId);
        }

        [HttpDelete("Delete{id:guid}")]
        public async Task<ActionResult<long>> DeleteAppointment(long id)
        {
            return Ok(await _service.DeleteAppointment(id));
        }
    }
}
