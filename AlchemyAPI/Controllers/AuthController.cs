using System.Security.Claims;
using Alchemy.Domain.Interfaces;
using AlchemyAPI.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AlchemyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserService userService, ILogger<AccountController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (succeded, errors) = await _userService.Register(
                request.UserName,
                request.Email,
                request.Password,
                request.FirstName,
                request.LastName);

            if (!succeded)
                return BadRequest(new { Errors = errors });

            return Ok(new { Message = "User registered successfully" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (token, error) = await _userService.Login(request.Email, request.Password);

            if (error != null)
                return Unauthorized(new { Errors = error });

            return Ok(new { Token = token });
        }

        [HttpGet("Profile")]
        [Authorize]
        public IActionResult GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

            if (userId == null)
                return Unauthorized();

            _logger.LogInformation($"User profile requested for user ID: {userId}");

            return Ok
            (
                new
                {
                    Id = userId,
                    UserName = userName,
                    Email = userEmail,
                    Roles = roles
                }
            );
        }
    }
}
