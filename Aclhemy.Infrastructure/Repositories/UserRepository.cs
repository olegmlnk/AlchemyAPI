using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using Alchemy.Infrastructure.Entities;
using Alchemy.Infrastructure.Mappings;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Alchemy.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AlchemyDbContext _alchemyDbContext;
        private readonly IMapper _mapper;
        public UserRepository(AlchemyDbContext alchemyDbContext, IMapper mapper)
        {
            _alchemyDbContext = alchemyDbContext;
            _mapper = mapper;
        }

        public async Task<List<UserEntity>> GetAllUsers()
        {
            var usersEntity = await _alchemyDbContext.Users
                .AsNoTracking()
                .Include(u => u.Appointments)
                .ToListAsync();

            var users = usersEntity
                .Select(u => new UserEntity
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    PasswordHash = u.PasswordHash,
                    Email = u.Email,
                    Appointments = u.Appointments.Select(a => new AppointmentEntity
                    {
                        Id = a.Id,
                        ScheduleSlotId = a.ScheduleSlotId,
                        Description = a.Description,
                        UserId = a.UserId,
                        ServiceId = a.ServiceId,
                        MasterId = a.MasterId
                    }).ToList()
                });

            return users.ToList();
        }
        public async Task<User> GetUserByEmail(string email)
        {
            var userEntity = await _alchemyDbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception("User not found");

            return _mapper.Map<User>(userEntity);
        }

        public async Task<long> DeleteUser(long id)
        {
            var user = await _alchemyDbContext.Users.FindAsync(id);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            _alchemyDbContext.Users.Remove(user);
            await _alchemyDbContext.SaveChangesAsync();

            return user.Id;
        }


        public async Task AddUser(User user)
        {
            var userEntity = new UserEntity
            {
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
                Appointments = user.Appointments.Select(a => new AppointmentEntity
                {
                    Id = a.Id,
                    ScheduleSlotId = a.ScheduleSlotId,
                    Description = a.Description,
                    UserId = a.UserId,
                    ServiceId = a.ServiceId,
                    MasterId = a.MasterId
                }).ToList()
            };

            await _alchemyDbContext.Users.AddAsync(userEntity);
            await _alchemyDbContext.SaveChangesAsync();
        }

    }
}
