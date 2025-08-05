
using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IUserRepository 
    {
        // Define any additional methods specific to user operations here
        // For example, you might want to add methods for user authentication, role management, etc.
        // This interface can be extended as needed to include more user-specific functionality.
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
    }
}