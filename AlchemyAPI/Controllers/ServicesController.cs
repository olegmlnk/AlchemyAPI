using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using Alchemy.Domain.Services;
using AlchemyAPI.Contracts;
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

        [HttpGet]
        public async Task<ActionResult<List<ServiceResponse>>> GetServices()
        {
            var services = await _servicesService.GetServices();

            var response = services.Select(s => new
            {
                s.Id,
                s.Title,
                s.Description,
                s.Price,
                s.Duration
            });

            return Ok(services);
        }

        [HttpGet("GetById{id:guid}")]
        public async Task<ActionResult<ServiceResponse>> GetServiceById(long id)
        {
            var service = await _servicesService.GetServiceById(id);
          
            return Ok(service);
        }


        [HttpPost("Create")]
        public async Task<ActionResult<long>> CreateService([FromBody] ServiceRequest request)
        {
            var (service, error) = Service.Create(0, request.Title, request.Description, request.Price, request.Duration);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var serviceId = await _servicesService.CreateService(service);

            return CreatedAtAction(nameof(GetServiceById), new { id = serviceId }, serviceId);
        }

        [HttpPut("Update{id:guid}")]
        public async Task<ActionResult<long>> UpdateService(long id, [FromBody] ServiceRequest request)
        {
           var serviceId = await _servicesService.UpdateService(id, request.Title, request.Description, request.Price, request.Duration);
            return Ok(serviceId);
        }

        [HttpDelete("Delete{id:guid}")]
        public async Task<ActionResult<long>> DeleteService(long id)
        {
            var serviceId = await _servicesService.DeleteService(id);
            return Ok(serviceId);
        }
    }
}
