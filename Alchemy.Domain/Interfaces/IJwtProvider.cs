using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(User user, IList<string> roles);
    }
}