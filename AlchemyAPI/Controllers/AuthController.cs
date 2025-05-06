using Alchemy.Domain.Interfaces;
using AlchemyAPI.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Alchemy.Domain.Models;

namespace AlchemyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger; 
        private readonly UserManager<User> _userManager;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerRequest)
        {
            var (success, errors) = await _userService.Register(registerRequest.UserName, registerRequest.Email, registerRequest.Password);
            if(!success)
                return BadRequest(new { errors });

            return Ok("Registration successful");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest loginRequest)
        {
            var (token, errors) = await _userService.Login(loginRequest.Email, loginRequest.Password);
            if (string.IsNullOrEmpty(token))
                return BadRequest(new { errors });

            return Ok(new { token });
        }

    }
}
