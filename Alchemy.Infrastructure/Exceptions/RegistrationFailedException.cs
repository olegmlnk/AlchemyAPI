namespace Alchemy.Infrastructure.Exceptions;

public class RegistrationFailedException(IEnumerable<string> errors) 
    : Exception($"Registration failed with errors: {string.Join(", ", errors)}");