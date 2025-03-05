﻿using Alchemy.Domain.Interfaces;
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

            var response = appointments.Select(a => new AppointmentResponse(a.Id, a.AppointmentDate, a.Description));
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateAppointment([FromBody] AppointmentRequest request)
        {
            var (appointment, error) = Appointment.Create(
                Guid.NewGuid(),
                request.AppointmentDate,
                request.Description);

            if(!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var appointmentId = await _service.CreateAppointment(appointment);

            return Ok(appointmentId);
        }

        [HttpPut("Update{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateAppointment(Guid id, [FromBody] AppointmentRequest request)
        {
            var appointmentId = await _service.UpdateAppointment(id, request.AppointmentDate, request.Description);

            return Ok(appointmentId);
        }

        [HttpDelete("Delete{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteAppointment(Guid id)
        {
            return Ok(await _service.DeleteAppointment(id));
        }
    }
}
