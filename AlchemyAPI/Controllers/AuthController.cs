using System.Security.Claims;
using Alchemy.Domain.Interfaces;
using AlchemyAPI.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Alchemy.Domain.Models;
using Alchemy.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace AlchemyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtHandler _jwtHandler;

        public AuthController(IUserService userService, ILogger<AuthController> logger, UserManager<User> userManager,
            IMapper mapper)
        {
            _userService = userService;
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest user)
        {
            if (user == null || !ModelState.IsValid)
                return BadRequest();

            var newUser = _mapper.Map<User>(user);
            var result = await _userManager.CreateAsync(newUser, user.Password!);
            var claim = new Claim(ClaimTypes.Email, user.Email);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { errors });
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var param = new Dictionary<string, string?>
            {
                { "email", newUser.Email },
                { "token", token }
            };

            if (newUser.Email == "admin@gmail.com")
                await _userManager.AddToRoleAsync(newUser, "Admin");

            else
                await _userManager.AddToRoleAsync(newUser, "User");

            await _userManager.AddClaimAsync(newUser, claim);

            return Ok(new RegisterUserResponse { IsSuccessful = true });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest user)
        {
            var existsUser = await _userManager.FindByEmailAsync(user.Email!);

            if (existsUser == null)
                return NotFound("User not found!");

            if (!await _userManager.IsEmailConfirmedAsync(existsUser))
                return Unauthorized(new LoginUserResponse
                    { IsLoginSuccessful = false, ErrorMessage = "Email is not confirmed" });

            if (!await _userManager.CheckPasswordAsync(existsUser, user.Password!))
            {
                await _userManager.AccessFailedAsync(existsUser);

                if (await _userManager.IsLockedOutAsync(existsUser))
                {
                    return Unauthorized(new LoginUserResponse
                        { IsLoginSuccessful = false, ErrorMessage = "Account is locked" });
                }

                return Unauthorized(new LoginUserResponse
                    { IsLoginSuccessful = false, ErrorMessage = "Invalid password" });
            }


            var token = await _jwtHandler.GenerateToken(existsUser);
            await _userManager.ResetAccessFailedCountAsync(existsUser);

            return Ok(new LoginUserResponse { IsLoginSuccessful = true, Token = token });
        }

        /*[HttpPost("external-login")]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalLoginRequest login)
        {
            var payload = await _jwtHandler.VerifyGoogleToken(login);
            if (payload == null) return BadRequest("Invalid external Authentication");

            var info = new UserLoginInfo(login.Provider!, payload.Subject, login.Provider);
            var newUser = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            if (newUser == null)
            {
                newUser = await _userManager.FindByEmailAsync(payload.Email);
                if (newUser == null)
                {
                    newUser = new User { Email = payload.Email, UserName = payload.Email, FirstName = payload.GivenName, LastName = payload.FamilyName };
                    await _userManager.CreateAsync(newUser);
                    if (newUser.Email == "admin@gmail.com") await _userManager.AddToRoleAsync(newUser, "Admin");
                    else await _userManager.AddToRoleAsync(newUser, "User");
                    await _userManager.AddLoginAsync(newUser, info);
                }

                else
                {
                    await _userManager.AddLoginAsync(newUser, info);
                }
            }

            if (newUser == null) return BadRequest("Invalid External Authentication");

            var token = await _jwtHandler.GenerateToken(newUser);

            return Ok(new LoginUserResponse { Token = token, IsLoginSuccessful = true });
        }
        
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest forgotPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existUser = await _userManager.FindByEmailAsync(forgotPassword.Email!);
            if(existUser == null) return NotFound("User not found");
            
            var token = await _userManager.GeneratePasswordResetTokenAsync(existUser);
            var param = new Dictionary<string, string>
            {
                { "token", token! },
                { "email", forgotPassword.Email! }
            };

            var callback = QueryHelpers.AddQueryString(forgotPassword.UserURI!, param!);

            return Ok("Token was sent!");
        }
        
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPassword)
        {
            if (!ModelState.IsValid) return BadRequest();

            var existUser = await _userManager.FindByEmailAsync(resetPassword.Email!);

            if (existUser == null) return NotFound("User not found");

            var resetPassResult = await _userManager.ResetPasswordAsync(existUser, resetPassword.Token!, resetPassword.Password!);

            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description);

                return BadRequest(new { Errors = errors });
            }

            await _userManager.SetLockoutEndDateAsync(existUser, new DateTime(2000, 1, 1));

            return Ok("Password has been changed successfully");
        }*/

    }
}
