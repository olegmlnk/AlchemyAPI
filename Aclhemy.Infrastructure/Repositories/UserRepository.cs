using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;

namespace Alchemy.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AlchemyDbContext _alchemyDbContext;
        public UserRepository(AlchemyDbContext alchemyDbContext)
        {
            alchemyDbContext = _alchemyDbContext;
        }

        public async Task<Guid> GetUser(Guid id)
        {
            var user = await _alchemyDbContext.Users.FindAsync(id);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            return user.Id;
        }

        public async Task<Guid> DeleteUser(Guid id)
        {
            var user = await _alchemyDbContext.Users.FindAsync(id);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            _alchemyDbContext.Users.Remove(user);
            await _alchemyDbContext.SaveChangesAsync();

            return user.Id;
        }


        public async Task<Guid> AddUser(User user)
        {
            if(user == null)
                throw new ArgumentNullException("User cannot be null");

            await _alchemyDbContext.Users.AddAsync(user);
            await _alchemyDbContext.SaveChangesAsync();
            return user.Id;
        }

    }
}
