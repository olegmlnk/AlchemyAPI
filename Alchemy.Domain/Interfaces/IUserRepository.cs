
using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(string id);
        Task<User?> GetUserByEmail(string email);
        Task<List<User>> GetAllUsers();
    }
}