using System.ComponentModel.DataAnnotations;

namespace AlchemyAPI.Contracts
{
    public record LoginUserRequest
    (
        [Required]
        string Email,
        [Required]
        string Password
    );
}
