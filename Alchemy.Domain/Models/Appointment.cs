using Microsoft.Identity.Client;

namespace Alchemy.Domain.Models
{
    public class Appointment
    {
        public const int MAX_DESCRIPTION_LENGTH = 255;
        private Appointment(Guid id, DateTime appointmentDate, string description, Guid masterId , Guid serviceId, Guid userId)
        {
            Id = id;
            AppointmentDate = appointmentDate;
            Description = description;
            MasterId = masterId;
            ServiceId = serviceId;
            UserId = userId;
        }

        public Guid Id { get; }
        public DateTime AppointmentDate { get; } = DateTime.Now;
        public string Description { get; } = string.Empty;
        public Guid MasterId { get; }
        public Master Master { get; }
        public Guid ServiceId { get; }
        public Service Service { get; }
        public Guid UserId { get; }
        public User User { get; }

        public static (Appointment Appointment, string Error) Create(Guid id, DateTime appointmentDate, string description, Guid masterId, Guid serviceId, Guid userId)
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

            var appointment = new Appointment(id, appointmentDate, description, masterId, serviceId, userId);

            return (appointment, error);
        }
    }
}
