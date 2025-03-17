using Microsoft.AspNetCore.Identity;

namespace Alchemy.Domain.Models
{
    public class User : IdentityUser
    {
        public User(Guid id, string firstName, string lastName, string email, ICollection<Appointment> Appointments)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Appointments = new List<Appointment>();
        }

        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public ICollection<Appointment> Appointments { get; set; }  

        public string FullName => $"{FirstName} {LastName}";
    }
}
