using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Alchemy.Infrastructure.Repositories
{
    [Authorize(Roles = "Admin")]
    public class UserRepository : IUserRepository
    {
        private readonly AlchemyDbContext _context;
        public UserRepository(AlchemyDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.FirstName)
                .Include(u => u.LastName)
                .Include(u => u.Appointments)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.NormalizedEmail == email) ?? throw new Exception("User not found");
        }
    }
}
