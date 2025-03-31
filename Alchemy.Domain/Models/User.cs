using Microsoft.AspNetCore.Identity;

namespace Alchemy.Domain.Models
{
    public class User : IdentityUser
    {
        public User(Guid id, string username, string passwordHash, string email, List<Appointment> Appointments)
        {
            Id = id;
            Username = username;
            PasswordHash = passwordHash;
            Email = email;
            Appointments = new List<Appointment>();
        }

        public Guid Id { get; set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public string Email { get; private set; }
        public List<Appointment> Appointments { get; set; }  


        public static (User User, string Error) Create(Guid id, string username, string passwordHash, string email, List<Appointment> appointments)
        {
            var error = string.Empty;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(username))
            {
                error = "First name and last name cannot be empty";
            }
            var user = new User(id, username, passwordHash, email, new List<Appointment>());
            return (user, error);
        }
    }
}
