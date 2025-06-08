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
        public virtual MasterSchedule ScheduleSlot { get; private set; } = null!;
        public string? Description { get; private set; } 
        public string UserId { get; private set; }
        public virtual User User { get; private set; } = null!;
        public long MasterId { get; private set; }
        public virtual  Master Master { get; private set; } = null!;
        public long ServiceId { get; private set; }
        public virtual  Service Service { get; private set; } = null!;

        public static (Appointment? Appointment, string? Error) Create(long scheduleSlotId, 
            string description, string userId, long masterId, long serviceId, MasterSchedule scheduleSlot, User user, Master master, Service service
             )
        {
            var errors = new List<string>();

            if (scheduleSlot == null)
                errors.Add("Schedule slot cannot be null");
            else if (scheduleSlot.Id != scheduleSlotId)
                errors.Add("Provided schedule slotId does not match the schedule slot object.");

            if (user == null)
                errors.Add("User cannot be null");
            else if (user.Id != userId)
                errors.Add("Provided userId does not match the user object.");

            if (master == null)
                errors.Add("Master cannot be null");
            else if (master.Id != masterId)
                errors.Add("Provided masterId does not match the master object.");

            if (service == null)
                errors.Add("Service cannot be null");
            else if (service.Id != serviceId)
                errors.Add("Provided serviceId does not match the service object");

            if (description.Length > MAX_DESCRIPTION_LENGTH)
                errors.Add("Description cannot be longer than 255 symbols");
            
            if (string.IsNullOrWhiteSpace(userId)) errors.Add("User ID cannot be empty.");
            if (masterId <= 0) errors.Add("Invalid Master ID.");
            if (serviceId <= 0) errors.Add("Invalid Service ID.");

            if (errors.Count > 0)
            {
                return (null, string.Join("; ", errors));
            }

            var appointment = new Appointment(scheduleSlotId, description, userId, masterId, serviceId)
            {
                ScheduleSlot = scheduleSlot,
                Description = description,
                User = user,
                Master = master,
                Service = service
            };
            return (appointment, null);
        }

        public (bool Success, string Description) UpdateDescription(string newDescription)
        {
            if (string.IsNullOrWhiteSpace(newDescription))
                return(false, "Description cannot be empty");
            if (newDescription.Length > MAX_DESCRIPTION_LENGTH)
                return (false, $"Description cannot be longer than {MAX_DESCRIPTION_LENGTH} characters.");

            Description = newDescription;
            return (true, null);
        }
    }
}
