namespace Alchemy.Domain.Models
{
    public class Master
    {
        public const int MAX_DESCRIPTION_LENGTH = 100;
        public const int MAX_EXPEIRENCE_LENGTH = 25;

        private Master() { }
        private Master(Guid id, string name, string expeirence, string description, List<Appointment> appointments)
        {
            Id = id;
            Name = name;
            Expeirence = expeirence;
            Description = description;
            Appointments = appointments;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Expeirence { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public List<Appointment> Appointments { get; private set; }

        public static (Master master, string error) Create(Guid id, string name, string expeirence, string description, List<Appointment> appointments)
        {
            var error = string.Empty;

            if(string.IsNullOrEmpty(description) || description.Length > MAX_DESCRIPTION_LENGTH)
            {
                Console.WriteLine("Description cannot be empty or longer than 100 symbols length");
            }

            if (string.IsNullOrEmpty(expeirence) || expeirence.Length > MAX_EXPEIRENCE_LENGTH)
            {
                Console.WriteLine("Expeirence cannot be empty or longer than 25 symbols length");
            }

            var master = new Master(id, name, expeirence, description, appointments);

            return (master, error);
        }
    }
}
