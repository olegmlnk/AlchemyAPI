
using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid> DeleteUser(Guid id);
        Task<Guid> GetUser(Guid id);
        Task<Guid> AddUser(User user);
    }
}