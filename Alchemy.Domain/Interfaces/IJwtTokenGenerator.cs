using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces;

public interface IJwtTokenGenerator
{
    Task<string> GenerateToken(User user);
}