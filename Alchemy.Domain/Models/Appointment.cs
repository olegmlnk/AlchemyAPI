namespace Alchemy.Domain.Models
{
    public class Appointment
    {
        public const int MAX_DESCRIPTION_LENGTH = 255;
        private Appointment(Guid id, DateTime appointmentDate, string description)
        {
            Id = id;
            AppointmentDate = appointmentDate;
            //ClientId = clientId;
            //MasterId = masterId;
            Description = description;
            //Client = client;
            //Master = master;
        }

        public Guid Id { get; }
        public DateTime AppointmentDate { get; } = DateTime.Now;
        //public Guid ClientId { get; }
        //public User Client { get; }
        //public Guid MasterId { get; }
        //public Master Master { get; }
        public string Description { get; } = string.Empty;

        public static (Appointment Appointment, string Error) Create(Guid id, DateTime appointmentDate, string description)
        {
            var error = string.Empty;

            if ((string.IsNullOrEmpty(description)) || description.Length > MAX_DESCRIPTION_LENGTH)
            {
                Console.WriteLine("Description cannot be empty or longer than 255 symbols");
            }

            var appointment = new Appointment(id, appointmentDate, description);

            return (appointment, error);
        }
    }
}
