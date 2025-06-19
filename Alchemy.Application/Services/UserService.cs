using Alchemy.Domain;
using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using Alchemy.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Alchemy.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private const string DefaultUserRole = "User";

        public UserService(UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = _userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }


        public async Task<(bool Success, IEnumerable<string> Errors)> Register(string username, string email, string password, string firstName, string lastName)
        {
            var (user, error) = User.Create(username, email, firstName, lastName);

            if (error != null)
                return (false, new[] { error });

            var result = await _userManager.CreateAsync(user!, password);

            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description));

            await _userManager.AddToRoleAsync(user!, DefaultUserRole);

            return (true, Enumerable.Empty<string>());
        }

        public async Task<(string? Token, string? Error)> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
                return (null, "Invalid email or password");

            var token = await _jwtTokenGenerator.GenerateToken(user);
            return (token, null);
        }
    }
} 
