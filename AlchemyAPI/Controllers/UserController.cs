using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using AlchemyAPI.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlchemyAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<User> _userManager;

        public UserController(IUserService userService, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userService = userService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerRequest)
        {
            await _userService.Register(registerRequest.Username, registerRequest.Email, registerRequest.Password);
            return Ok("User has been registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest loginRequest)
        {
            await _userService.Login(loginRequest.Email, loginRequest.Password);
            return Ok("User has been logged in");
        }

        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole([FromBody] string role)
        {
            return Ok();
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] string role)
        {
            return Ok();
        }
    }
}
