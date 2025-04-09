using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using Alchemy.Infrastructure;
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
        private readonly UserManager<User> _userManager;
        private readonly JwtProvider _jwtProvider;

        public UserController(IUserService userService, UserManager<User> userManager, RoleManager<IdentityRole<long>> roleManager)
        {
            _userService = userService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerRequest)
        {
            var user = new User
            {
                UserName = registerRequest.Username,
                Email = registerRequest.Email
            };

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, "Client");

            return Ok("Registration successfull");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);

            if(user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
                return Unauthorized("Check your email or password");

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtProvider.GenerateToken(user, roles);

            return Ok(new { token });

        }

        //[HttpPost("add-role")]
        //public async Task<IActionResult> AddRole([FromBody] string role)
        //{
        //    if(!await _roleManager.RoleExistsAsync(role))
        //    {
        //        var result = await _roleManager.CreateAsync(new IdentityRole<long>(role));

        //        if (result.Succeeded)
        //            return Ok(new { message = "Role added successfully" });

        //        return BadRequest(result.Errors);
        //    }

        //    return BadRequest("Role already exists");
        //}

        //[HttpPost("assign-role")]
        //[Authorize(Roles = "Admin")] 
        //public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
        //{
        //    var user = await _userManager.FindByNameAsync(request.UserName);
        //    if (user == null)
        //        return BadRequest("User not found");

        //    var roleExists = await _roleManager.RoleExistsAsync(request.Role);
        //    if (!roleExists)
        //        return BadRequest("Role does not exist");

        //    var result = await _userManager.AddToRoleAsync(user, request.Role);
        //    if (result.Succeeded)
        //        return Ok(new { message = "Role assigned successfully" });

        //    return BadRequest(result.Errors);
        //}

    }
}
