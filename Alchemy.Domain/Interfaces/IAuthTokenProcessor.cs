using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces;

public interface IAuthTokenProcessor
{
    (string jwtToken, DateTime expiresAtUtc) GenerateJwtToken(User user);
    
    string GenerateRefreshToken();
    
    void WriteAuthTokenToHttpOnlyCookie(string cookieName, string token, DateTime expiration);
}