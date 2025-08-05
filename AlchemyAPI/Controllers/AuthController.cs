using System.Security.Claims;
using Alchemy.Domain.Contracts;
using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace AlchemyAPI.Controllers
{
    [ApiController]
    [Route("api/account/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserService userService, ILogger<AccountController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest request)
        {
            await _userService.RegisterAsync(request);
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequest request)
        {
            await _userService.LoginAsync(request);
            return Ok("User logged in successfully.");
        }

        [HttpGet("login/google")]
        public IActionResult GoogleLogin([FromQuery] string returnUrl, LinkGenerator linkGenerator, SignInManager<User> signInManager, HttpContext context)

        {
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google",
                linkGenerator.GetPathByName(context, "GoogleLoginCallback")
                + $"?returnUrl={returnUrl}");

            return Challenge(properties, ["Google"]);
        }
    

        [HttpGet("login/google/callback", Name = "GoogleLoginCallback")]
        public async Task<IActionResult> GoogleCallbackAsync([FromQuery] string returnUrl, HttpContext httpContext,
            IUserService accountService)
        {
            var result = await httpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            await accountService.LoginWithGoogleAsync(result.Principal);

            return Redirect(returnUrl);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(HttpContext httpContext)
        {
            var refreshToken = httpContext.Request.Cookies["REFRESH_TOKEN"];
            await _userService.RefreshTokenAsync(refreshToken);
        
            return Ok("Refresh token processed successfully.");
        }
    
        [HttpGet("get-movies")]
        [Authorize]
        public async Task<IActionResult> GetMoviesAsync()
        {
            var movies = new List<string> { "Movie 1", "Movie 2", "Movie 3" };
        
            return Ok(movies);
        }
        
    }
}
