using Alchemy.Domain;
using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Alchemy.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly ILogger<UserService> _logger;
        public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtProvider jwtProvider, ILogger<UserService> logger)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _logger = logger;
        }

        public async Task Register(string username, string email, string password)
        {
            var hashedPassword = _passwordHasher.GenerateHash(password);

            var (user, error) = User.Create(username, hashedPassword, email, new List<Appointment>());
            if (error != null)
            {
                _logger.LogError("User creation failed: {Error}", error);
                throw new Exception(error);
            }

            await _userRepository.AddUser(user);
        }


        public async Task<string> Login(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (!result)
                throw new Exception("Failed to login");

            return _jwtProvider.GenerateToken(user);
        }
    }
} 
