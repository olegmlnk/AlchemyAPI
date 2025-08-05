using System.ComponentModel.DataAnnotations;

namespace Alchemy.Domain.Contracts
{
    public class LoginUserRequest
    {
       public required string Email { get; init; }
       public required string Password { get; init; }
    };
}
