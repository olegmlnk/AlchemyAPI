using System.ComponentModel.DataAnnotations;

namespace AlchemyAPI.Contracts
{
    public record RegisterUserRequest
    (
        [Required] string Username,
        [Required] string Email,
        [Required] string Password
    );
}
