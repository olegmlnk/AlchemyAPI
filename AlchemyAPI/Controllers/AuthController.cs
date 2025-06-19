using System.Security.Claims;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using Alchemy.Domain.Interfaces;
using AlchemyAPI.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Alchemy.Domain.Models;
using Alchemy.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace AlchemyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public AuthController(IUserService userService, ILogger logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
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
        [AllowAnonymous]
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
