using Alchemy.Application.Services;
using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alchemy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IMasterScheduleService _masterScheduleService;

        public AppointmentController(IAppointmentService appointmentService, IMasterScheduleService masterScheduleService)
        {
            _appointmentService = appointmentService;
            _masterScheduleService = masterScheduleService;
        }

        // GET: api/Appointment
        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        // GET: api/Appointment/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(long id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return NotFound("Appointment not found");

            return Ok(appointment);
        }

        // POST: api/Appointment
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] Appointment appointment)
        {
            // Спочатку перевіряємо, чи є доступний слот у MasterSchedule
            var schedule = await _masterScheduleService.GetByIdAsync(appointment.ScheduleSlotId);
            if (schedule == null || schedule.IsBooked)
                return BadRequest("Slot is not available or already booked.");

            // Створюємо запис про бронювання
            var createdAppointmentId = await _appointmentService.CreateAppointmentAsync(appointment);
            if (createdAppointmentId == null)
                return BadRequest("Failed to create appointment.");

            // Оновлюємо статус слота
            schedule.IsBooked = true;
            await _masterScheduleService.MarkSlotAsBookedAsync(schedule.Id);

            return CreatedAtAction(nameof(GetAppointmentById), new { id = createdAppointmentId }, appointment);
        }

        // PUT: api/Appointment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(long id, [FromBody] Appointment appointment)
        {
            if (id != appointment.Id)
                return BadRequest("Appointment ID mismatch.");

            var updatedAppointmentId = await _appointmentService.UpdateAppointmentAsync(
                id,
                appointment.ScheduleSlotId.ToString(),
                appointment.Description,
                appointment.MasterId,
                appointment.ServiceId,
                appointment.UserId
            );
            if (updatedAppointmentId == null)
                return NotFound("Appointment not found");

            return NoContent();
        }

        // DELETE: api/Appointment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelAppointment(long id)
        {
            var isCancelled = await _appointmentService.CancelAppointmentAsync(id);
            if (!isCancelled)
                return NotFound("Appointment not found");

            // Якщо запис було скасовано, звільняємо слот в MasterSchedule
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return NotFound("Appointment not found");

            var schedule = await _masterScheduleService.GetByIdAsync(appointment.ScheduleSlotId);
            if (schedule == null)
                return NotFound("Schedule slot not found");

            schedule.IsBooked = false;
            await _masterScheduleService.MarkSlotAsAvailableAsync(schedule.Id);

            return NoContent();
        }
    }
}
