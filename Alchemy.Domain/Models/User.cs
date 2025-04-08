using Microsoft.AspNetCore.Identity;

namespace Alchemy.Domain.Models
{
    public class User : IdentityUser<long>
    {
        public User() { }

        public User(string username, string passwordHash, string email, List<Appointment> appointments, string role)
        {
            UserName = username; 
            PasswordHash = passwordHash;
            Email = email;
            Role = role;
            Appointments = appointments ?? new List<Appointment>(); 
        }

        public List<Appointment> Appointments { get; private set; }
        public string Role { get; private set; } = string.Empty;

        public static (User User, string Error) Create(string username, string passwordHash, string email, List<Appointment> appointments, string role)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return (null, "Username cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                return (null, "Password cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                return (null, "Email cannot be empty");
            }

            var user = new User(username, passwordHash, email, appointments, role);
            return (user, null);
        }
    }
}
