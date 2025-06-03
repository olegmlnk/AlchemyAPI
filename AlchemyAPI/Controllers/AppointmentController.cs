using Alchemy.Application.Services;
using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using AlchemyAPI.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alchemy.API.Controllers
{
    [Route("api/")]
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

        [HttpGet("GetAllAppointments")]
        public async Task<IActionResult> GetAppointments()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        [HttpGet("GetAppointmentById")]
        public async Task<IActionResult> GetAppointmentById(long id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return NotFound("Appointment not found");

            return Ok(appointment);
        }

        [HttpPost("CreateAppointment")]
          public async Task<IActionResult> CreateAppointment([FromBody] AppointmentRequest request)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var scheduleSlot = await _masterScheduleService.GetByIdAsync(request.ScheduleSlotId);
            if (scheduleSlot == null)
            {
                return BadRequest("Schedule slot not found.");
            }
            if (scheduleSlot.IsBooked)
            {
                return BadRequest("Slot is not available or already booked.");
            }

            var (appointmentDomainModel, error) = Appointment.Create(
                request.ScheduleSlotId,
                request.Description,
                request.UserId, 
                request.MasterId,
                request.ServiceId
            );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            if (appointmentDomainModel == null)
            {
                 return BadRequest("Failed to create appointment domain model due to validation.");
            }


            try
            {
                var createdAppointmentId = await _appointmentService.CreateAppointmentAsync(appointmentDomainModel);
                if (createdAppointmentId == null)
                {
                    return BadRequest("Failed to create appointment.");
                }
                
                // Оскільки репозиторій вже оновив MasterSchedule, додатковий виклик тут не потрібен.

                // Створюємо об'єкт для відповіді, якщо потрібно (замість повернення всього appointmentDomainModel)
                var response = new AppointmentResponse // Або інший відповідний DTO
                {
                    Id = createdAppointmentId.Value,
                    ScheduleSlotId = appointmentDomainModel.ScheduleSlotId,
                    Description = appointmentDomainModel.Description,
                    UserId = appointmentDomainModel.UserId,
                    MasterId = appointmentDomainModel.MasterId,
                    ServiceId = appointmentDomainModel.ServiceId
                };


                return CreatedAtAction(nameof(GetAppointmentById), new { id = createdAppointmentId.Value }, response);
            }
            catch (KeyNotFoundException knfEx) // Наприклад, якщо сервіс/репозиторій кидає це для залежностей
            {
                return NotFound(knfEx.Message);
            }
            catch (InvalidOperationException ioEx) // Наприклад, якщо слот став заброньованим паралельно
            {
                return Conflict(ioEx.Message); // 409 Conflict
            }
            catch (Exception ex)
            {
                // Логування помилки ex
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while creating the appointment.");
            }
        }

        // PUT: api/Appointment/5
        [HttpPut("UpdateAppointment")]
        public async Task<long> UpdateAppointment(long id, [FromBody] AppointmentRequest appointment)
        {
            if (id != appointment.ScheduleSlotId)
                return BadRequest("Appointment ID mismatch.");

            var updatedAppointmentId = await _appointmentService.UpdateAppointmentAsync(
                id,
                appointment.ScheduleSlotId,
                appointment.Description,
                appointment.UserId,
                appointment.MasterId,
                appointment.ServiceId
            );
            if (updatedAppointmentId == null)
                return NotFound("Appointment not found");

            return id, appointment;
        }

        // DELETE: api/Appointment/5
        [HttpDelete("CancelAppointment")]
        public async Task<IActionResult> CancelAppointment(long id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return NotFound("Appointment not found");
            
            var isCancelled = await _appointmentService.CancelAppointmentAsync(id);
            if (!isCancelled)
                return NotFound("Appointment not found");

            // Якщо запис було скасовано, звільняємо слот в MasterSchedule

            var schedule = await _masterScheduleService.GetByIdAsync(appointment.ScheduleSlotId);
            if (schedule == null)
                return NotFound("Schedule slot not found");

            schedule.IsBooked = false;
            await _masterScheduleService.MarkSlotAsAvailableAsync(schedule.Id);

            return NoContent();
        }
    }
}
