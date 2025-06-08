using Microsoft.AspNetCore.Identity;

namespace Alchemy.Domain.Models
{
    public class User : IdentityUser
    {
        public const int MAX_NAME_LENGTH = 50;
        
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        private readonly List<Appointment> _appointment = new List<Appointment>();
        public IReadOnlyList<Appointment> Appointments => _appointment.AsReadOnly();

        private User(string userName, string email, string firstName, string lastName) 
        : base(userName)
        {
            Email = email;
            NormalizedEmail = email.ToUpperInvariant();
            NormalizedUserName = userName.ToUpperInvariant();
            FirstName = firstName;
            LastName = lastName;
        }

        public User()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
        }

        public static (User? User, string? Error) Create(string userName, string email, string firstName,
            string lastName)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(userName))
                errors.Add("UserName cannot be empty.");

            if (string.IsNullOrWhiteSpace(email))
                errors.Add("Email cannot be empty.");

            if (string.IsNullOrWhiteSpace(firstName))
                errors.Add("FirstName cannot be empty.");
            else if (firstName.Length > MAX_NAME_LENGTH)
                errors.Add($"FirstName cannot be larger than {MAX_NAME_LENGTH} symbols.");
            
            if (string.IsNullOrWhiteSpace(lastName))
                errors.Add("Last name cannot be empty.");
            else if (lastName.Length > MAX_NAME_LENGTH)
                errors.Add($"Last name cannot be longer than {MAX_NAME_LENGTH} symbols.");

            if (errors.Any())
                return (null, string.Join("; ", errors));

            var user = new User(userName, email, firstName, lastName);

            return (user, null);
        }

        public (bool Success, string? Error) UpdateProfile(string firstName, string lastName)
        {
            var errors = new List<string>();
            
            if (string.IsNullOrWhiteSpace(firstName))
                errors.Add("FirstName cannot be empty.");
            else if (firstName.Length > MAX_NAME_LENGTH)
                errors.Add($"FirstName cannot be larger than {MAX_NAME_LENGTH} symbols.");
            
            if (string.IsNullOrWhiteSpace(lastName))
                errors.Add("Last name cannot be empty.");
            else if (lastName.Length > MAX_NAME_LENGTH)
                errors.Add($"Last name cannot be longer than {MAX_NAME_LENGTH} symbols.");

            if (errors.Any())
                return (false, string.Join("; ", errors));

            FirstName = firstName;
            LastName = lastName;
            return (true, null);
        }
    }
}
