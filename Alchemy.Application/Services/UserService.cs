using Alchemy.Domain;
using Alchemy.Domain.Models;

namespace Alchemy.Application.Services
{
    public class UserService
    {
        private readonly IPasswordHasher _passwordHasher;
        public UserService(IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public async Task Register(string username, string email, string password)
        {
            var hashedPassword  = _passwordHasher.GenerateHash(password);

            var user = User.Create(Guid.NewGuid(), username, hashedPassword, email, new List<Appointment>());
        }
    }
}
