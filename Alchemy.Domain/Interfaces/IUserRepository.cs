
using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<long> DeleteUser(long id);
        Task<User> GetUserByEmail(string email);
        Task AddUser(User user);
    }
}