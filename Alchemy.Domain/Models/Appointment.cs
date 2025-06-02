namespace Alchemy.Domain.Models
{
    public class Appointment
    {
        public const int MAX_DESCRIPTION_LENGTH = 255;

        public Appointment() { }
        public Appointment(long scheduleSlotId, string description, string userId, long masterId, long serviceId)
        {
            ScheduleSlotId = scheduleSlotId;
            Description = description;
            MasterId = masterId;
            ServiceId = serviceId;
            UserId = userId;
        }

        public long Id { get; private set; }
        public long ScheduleSlotId { get; private set; }
        public  MasterSchedule ScheduleSlot { get;  set; }
        public string Description { get; private set; } = string.Empty;
        public string UserId { get; private set; }
        public  User User { get;  set; }
        public long MasterId { get; private set; }
        public  Master Master { get;  set; }
        public long ServiceId { get; private set; }
        public  Service Service { get;  set; }

        public static (Appointment? Appointment, string Error) Create(long scheduleSlotId, string description, string userId, long masterId, long serviceId)
        {
            if (string.IsNullOrWhiteSpace(description))
                return (null, "Description cannot be empty.");

            if (description.Length > MAX_DESCRIPTION_LENGTH)
                return (null, $"Description cannot be longer than {MAX_DESCRIPTION_LENGTH} characters.");

            var appointment = new Appointment(scheduleSlotId, description, userId, masterId, serviceId);
            return (appointment, null);
        }
    }
}
