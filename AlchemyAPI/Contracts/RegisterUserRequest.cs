using System.ComponentModel.DataAnnotations;

namespace AlchemyAPI.Contracts
{
    public class RegisterUserRequest
    {
       [Required]
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [Required] 
        [EmailAddress] 
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public string? UserURI { get; set; }
    }
}
