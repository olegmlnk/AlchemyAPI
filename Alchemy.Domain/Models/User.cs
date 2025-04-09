using Microsoft.AspNetCore.Identity;

namespace Alchemy.Domain.Models
{
    public class User : IdentityUser<long>
    {
        public User() { }

        public User(string username, string passwordHash, string email, ICollection<Appointment>? appointments = null)
        {
            UserName = username;
            PasswordHash = passwordHash;
            Email = email;
            Appointments = appointments ?? new List<Appointment>();
        }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public static (User? User, string? Error) Create(string username, string passwordHash, string email, ICollection<Appointment>? appointments = null)
        {
            if (string.IsNullOrWhiteSpace(username))
                return (null, "Username cannot be empty");

            if (string.IsNullOrWhiteSpace(passwordHash))
                return (null, "Password cannot be empty");

            if (string.IsNullOrWhiteSpace(email))
                return (null, "Email cannot be empty");

            return (new User(username, passwordHash, email, appointments), null);
        }
    }
}
