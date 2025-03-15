namespace Alchemy.Domain.Models
{
    public class User
    {
        public User(Guid id, string firstName, string lastName, string email, List<Appointment> Appointments)
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
        public List<Appointment> Appointments { get; set; }  

        public string FullName => $"{FirstName} {LastName}";
    }
}
