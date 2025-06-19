using Alchemy.Domain.Interfaces;
using AlchemyAPI.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlchemyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesService _servicesService;

        public ServicesController(IServicesService servicesService)
        {
            _servicesService = servicesService;
        }

        [HttpGet("GetServices")]
        [AllowAnonymous]
        public async Task<IActionResult> GetServices()
        {
            var services = await _servicesService.GetServices();

            var response = services.Select(s => new ServiceResponse(
                s.Id,
                s.Title,
                s.Description,
                s.Price,
                s.Duration.TotalHours));

            return Ok(response);
        }

        [HttpGet("GetServiceById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetServiceById(long id)
        {
            var service = await _servicesService.GetServiceById(id);

            if (service == null)
                return NotFound();

            var response = new ServiceResponse(
                service.Id,
                service.Title,
                service.Description,
                service.Price,
                service.Duration.TotalHours);

            return Ok(response);
        }


        [HttpPost("CreateService")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateService([FromBody] ServiceRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var duration = TimeSpan.FromMinutes(request.DurationInMinutes);
            var (serviceId, error) = await _servicesService.CreateService(
                request.Title,
                request.Description,
                request.Price,
                duration);

            if (error != null)
                return BadRequest(new { Error = error });

            return CreatedAtAction(nameof(GetServiceById), new { id = serviceId });
        }

        [HttpPut("UpdateService")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateService(long id, [FromBody] ServiceRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var duration = TimeSpan.FromMinutes(request.DurationInMinutes);
            var (success, error) = await _servicesService.UpdateService(
                id,
                request.Title,
                request.Description,
                request.Price,
                duration);

            if (error != null)
                return NotFound(new { Error = error });

            if (!success)
                return BadRequest(new { Error = "Falied to update service" });

            return NoContent();
        }

        [HttpDelete("DeleteService")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<long>> DeleteService(long id)
        {
            var success = await _servicesService.DeleteService(id);

            if (!success)
                return NotFound(new { Error = "Service not found or could not be deleted" });

            return NoContent();
        }
    }
}
