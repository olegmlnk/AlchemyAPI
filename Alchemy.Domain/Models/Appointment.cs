
namespace Alchemy.Domain.Models
{
    public class Appointment
    {
        public const int MAX_DESCRIPTION_LENGTH = 255;

        private Appointment() { }
        private Appointment(long scheduleSlotId, string description, long masterId , long serviceId, long userId)
        {
            ScheduleSlotId = scheduleSlotId;
            Description = description;
            MasterId = masterId;
            ServiceId = serviceId;
            UserId = userId;
        }

        public long Id { get; private set; }

        public long ScheduleSlotId { get; private set; }
        public MasterSchedule ScheduleSlot { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public long UserId { get; private set; }
        public User User { get; set; }
        public long MasterId { get; private set; }
        public Master Master { get; private set; }
        public long ServiceId { get; private set; }
        public Service Service { get; private set; }


        public static (Appointment Appointment, string error) Create(
       long scheduleSlotId, string description, long userId, long masterId, long serviceId)
        {
            var error = string.Empty;

            if (string.IsNullOrWhiteSpace(description))
                error = "Description cannot be empty.";

            var appointment = new Appointment
            {
                ScheduleSlotId = scheduleSlotId,
                Description = description,
                UserId = userId,
                MasterId = masterId,
                ServiceId = serviceId
            };

            return (appointment, error);
        }
    }
}
