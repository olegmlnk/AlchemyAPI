using System.ComponentModel.DataAnnotations;

namespace AlchemyAPI.Contracts
{
    public class LoginUserRequest
    {
        [Required] 
        [EmailAddress] 
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    };
}
