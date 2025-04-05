
namespace Alchemy.Domain.Models
{
    public class Appointment
    {
        public const int MAX_DESCRIPTION_LENGTH = 255;

        private Appointment() { }
        private Appointment(DateTime appointmentDate, string description, long masterId , long serviceId, long userId)
        {
            AppointmentDate = appointmentDate;
            Description = description;
            MasterId = masterId;
            ServiceId = serviceId;
            UserId = userId;
        }

        public long Id { get; private set; }
        public DateTime AppointmentDate { get; private set; } = DateTime.Now;
        public string Description { get; private set; } = string.Empty;
        public long MasterId { get; private set; }
        public Master Master { get; private set; }
        public long ServiceId { get; private set; }
        public Service Service { get; private set; }
        public long UserId { get; private set; }
        public User User { get; set; }

        public static (Appointment Appointment, string Error) Create(DateTime appointmentDate, string description, long masterId, long serviceId, long userId)
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

            var appointment = new Appointment(appointmentDate, description, masterId, serviceId, userId);

            return (appointment, error);
        }
    }
}
