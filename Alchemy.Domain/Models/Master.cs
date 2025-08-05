namespace Alchemy.Domain.Models
{
    public class Master
    {
        public const int MAX_NAME_LENGTH = 55;
        public const int MAX_DESCRIPTION_LENGTH = 100;
        public const int MAX_EXPERIENCE_LENGTH = 25;
        
        private Master() { }

        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Experience { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

        private readonly List<Appointment> _appointments = new List<Appointment>();
        public IReadOnlyList<Appointment> Appointments => _appointments.AsReadOnly();

        private readonly List<MasterSchedule> _masterSchedules = new List<MasterSchedule>();
        public IReadOnlyList<MasterSchedule> MasterSchedules => _masterSchedules.AsReadOnly();

        public static (Master? Master, string? Error) Create(Guid id, string name, string experience, string description)
        {
            var errors = new List<string>();
            
            if(id == Guid.Empty)
                errors.Add("Invalid master ID.");

            if (string.IsNullOrWhiteSpace(name))
                errors.Add("Master name cannot be empty.");
            else if (name.Length > MAX_NAME_LENGTH)
                errors.Add($"Master name cannot be Guider than {MAX_NAME_LENGTH} characters.");

            if (string.IsNullOrWhiteSpace(experience)) 
                errors.Add("Master experience description cannot be empty.");
            else if (experience.Length > MAX_EXPERIENCE_LENGTH)
                errors.Add($"Master experience description cannot be Guider than {MAX_EXPERIENCE_LENGTH} characters.");

            if (string.IsNullOrWhiteSpace(description))
                errors.Add("Master description cannot be empty.");
            else if (description.Length > MAX_DESCRIPTION_LENGTH)
                errors.Add($"Master description cannot be Guider than {MAX_DESCRIPTION_LENGTH} characters.");

            if (errors.Any())
            {
                return (null, string.Join("; ", errors));
            }

            var master = new Master
            {
                Id = id,
                Name = name,
                Experience = experience,
                Description = description
            };
            
            return (master, null);
        }
    }
}
