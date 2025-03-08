namespace Alchemy.Domain.Models
{
    public class Appointment
    {
        public const int MAX_DESCRIPTION_LENGTH = 255;
        private Appointment(Guid id, DateTime appointmentDate, string description, Guid masterId)
        {
            Id = id;
            AppointmentDate = appointmentDate;
            Description = description;
            MasterId = masterId;
        }

        public Guid Id { get; }
        public DateTime AppointmentDate { get; } = DateTime.Now;
        public string Description { get; } = string.Empty;
        public Guid MasterId { get; }
        public Master Master { get; }

        public static (Appointment Appointment, string Error) Create(Guid id, DateTime appointmentDate, string description, Guid masterId)
        {
            var error = string.Empty;

            if ((string.IsNullOrEmpty(description)) || description.Length > MAX_DESCRIPTION_LENGTH)
            {
                Console.WriteLine("Description cannot be empty or longer than 255 symbols");
            }

            if(appointmentDate < DateTime.Now)
            {
                return(null, "Appointment date cannot be in the past");
            }

            var appointment = new Appointment(id, appointmentDate, description, masterId);

            return (appointment, error);
        }
    }
}
