namespace Alchemy.Infrastructure.Exceptions;

public class RefreshTokenException(string message) : Exception($"Refresh token error: {message}");