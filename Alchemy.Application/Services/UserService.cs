using Alchemy.Domain;
using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using Alchemy.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Alchemy.Application.Services
{
    public class UserService : IUserService 
    {
        private readonly JwtHandler _jwtHandler;
        private readonly UserManager<User> _userManager;
        public UserService(JwtHandler jwtHandler, UserManager<User> userManager)
        {
            _jwtHandler = jwtHandler;
            _userManager = userManager;
        }

        public async Task<(bool Success, IEnumerable<string> Errors)> Register(string username, string email, string password)
        {
            var user = new User
            {
                UserName = username,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password); 

            if(!result.Succeeded)
            {
                return (false, result.Errors.Select(e => e.Description));
            }

            await _userManager.AddToRoleAsync(user, "Client");
            return (true, null);
        }


        public async Task<(string Token, string Error)> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if(user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                return (null, "Check your email or password");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = await _jwtHandler.GenerateToken(user);

            return (token, null);
        }
    }
} 
