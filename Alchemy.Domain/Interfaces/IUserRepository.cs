
using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<string> DeleteUser(string id);
        Task<User> GetUserByEmail(string email);
        Task AddUser(User user);
    }
}