namespace Alchemy.Domain.Models
{
    public class Client
    {
        public const int MAX_FULLNAME_LENGTH = 55;
        public Client() { }
        private Client(Guid id, string fullName, string email, string passwordHash, string role)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
        }

        public Guid Id { get; private set; }
        public string FullName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string Role {  get; private set; } = string.Empty;

        public static (Client Client, string Error) Create(Guid id, string fullName, string email, string passwordHash, string role)
        {
            var error = string.Empty;

            if (string.IsNullOrWhiteSpace(fullName) || fullName.Length > MAX_FULLNAME_LENGTH)
            {
                Console.WriteLine("Full name cannot be empty or longer than 55 sumbols");
            }

            var user = new Client(id, fullName, email, passwordHash, role);

            return (user, error);
        }
    }
}
