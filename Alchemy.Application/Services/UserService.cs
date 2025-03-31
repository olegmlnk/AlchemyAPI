using Alchemy.Domain;
using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;

namespace Alchemy.Application.Services
{
    public class UserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }

        public async Task Register(string username, string email, string password)
        {
            var hashedPassword = _passwordHasher.GenerateHash(password);

            var (user, error) = User.Create(Guid.NewGuid(), username, hashedPassword, email, new List<Appointment>());
            if (error != null)
            {
                throw new Exception(error);
            }

            await _userRepository.AddUser(user);
        }
    }
}
