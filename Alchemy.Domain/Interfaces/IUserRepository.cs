
using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid> DeleteUser(Guid id);
        Task<User> GetUserByEmail(string email);
        Task AddUser(User user);
    }
}