using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using Alchemy.Infrastructure.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Alchemy.Infrastructure.Repositories
{
    [Authorize(Roles = "Admin")]
    public class UserRepository : IUserRepository
    {
        private readonly AlchemyDbContext _context;
        private readonly IMapper _mapper;
        public UserRepository(AlchemyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var userEntities = await _context.Users
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<User>>(userEntities);
        }

        public async Task<User?> GetUserById(string id)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .Include(u => u.FirstName)
                .Include(u => u.LastName)
                .Include(u => u.Appointments)
                .ToListAsync();

            return _mapper.Map<User>(userEntity);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.NormalizedEmail == email) ?? throw new Exception("User not found");

            return _mapper.Map<User>(userEntity);
        }
    }
}
