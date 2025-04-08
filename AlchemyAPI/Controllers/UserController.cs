using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using AlchemyAPI.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlchemyAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole<long>> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, UserManager<User> userManager, RoleManager<IdentityRole<long>> roleManager, IConfiguration configuration)
        {
            _userService = userService;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
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
            if(!await _roleManager.RoleExistsAsync(role))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole<long>(role));

                if (result.Succeeded)
                    return Ok(new { message = "Role added successfully" });

                return BadRequest(result.Errors);
            }

            return BadRequest("Role already exists");
        }

        [HttpPost("assign-role")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return BadRequest("User not found");

            var roleExists = await _roleManager.RoleExistsAsync(request.Role);
            if (!roleExists)
                return BadRequest("Role does not exist");

            var result = await _userManager.AddToRoleAsync(user, request.Role);
            if (result.Succeeded)
                return Ok(new { message = "Role assigned successfully" });

            return BadRequest(result.Errors);
        }

    }
}
