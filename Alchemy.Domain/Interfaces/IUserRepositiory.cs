using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IUserRepositiory
    {
        Task<List<User>> GetUsersAsync();
        Task<Guid> GetUserAsync(Guid id);
        Task<Guid> AddAsync(User user);
        Task<Guid> RemoveAsync(Guid id);

    }
}
