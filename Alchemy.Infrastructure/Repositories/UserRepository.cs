using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Alchemy.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AlchemyDbContext _context;
        public UserRepository(AlchemyDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }
    }
}
