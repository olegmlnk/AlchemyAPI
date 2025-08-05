namespace Alchemy.Infrastructure.Exceptions;

public class UserAlreadyExistsException(string Email) : 
    Exception($"User with email {Email} already exists.");