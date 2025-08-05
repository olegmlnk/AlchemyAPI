using System.ComponentModel.DataAnnotations;

namespace Alchemy.Domain.Contracts;

public class ForgotPasswordRequest
{
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? UserURI { get; set; }
}