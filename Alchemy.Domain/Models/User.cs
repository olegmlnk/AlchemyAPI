using Microsoft.AspNetCore.Identity;

namespace Alchemy.Domain.Models
{
    public class User : IdentityUser<long>
    {
        public User() { }

        public User(string username, string passwordHash, string email, List<Appointment> appointments)
        {
            UserName = username; 
            PasswordHash = passwordHash;
            Email = email;
            Appointments = appointments ?? new List<Appointment>(); 
        }

        public List<Appointment> Appointments { get; private set; }

        public static (User User, string Error) Create(string username, string passwordHash, string email, List<Appointment> appointments)
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

            var user = new User(username, passwordHash, email, appointments);
            return (user, null);
        }
    }
}
