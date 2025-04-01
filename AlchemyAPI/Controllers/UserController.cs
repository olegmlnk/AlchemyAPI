using Alchemy.Application.Services;
using AlchemyAPI.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AlchemyAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
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

        //need to add CreateRole and AssignRoleToUser methods
    }
}
