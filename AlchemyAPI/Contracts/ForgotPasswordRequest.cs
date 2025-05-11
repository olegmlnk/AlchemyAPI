using System.ComponentModel.DataAnnotations;

namespace AlchemyAPI.Contracts;

public class ForgotPasswordRequest
{
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? UserURI { get; set; }
}