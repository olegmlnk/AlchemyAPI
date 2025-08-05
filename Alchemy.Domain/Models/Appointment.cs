namespace Alchemy.Domain.Models
{
    public class Appointment
    {
        public const int MAX_DESCRIPTION_LENGTH = 255;

        public Appointment() { }
        public Appointment(Guid scheduleSlotId, string description, Guid userId, Guid masterId, Guid serviceId)
        {
            ScheduleSlotId = scheduleSlotId;
            Description = description;
            MasterId = masterId;
            ServiceId = serviceId;
            UserId = userId;
        }

        public Guid Id { get; private set; }
        public Guid ScheduleSlotId { get; private set; }
        public virtual MasterSchedule ScheduleSlot { get; private set; } = null!;
        public string? Description { get; private set; } 
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; } = null!;
        public Guid MasterId { get; private set; }
        public virtual  Master Master { get; private set; } = null!;
        public Guid ServiceId { get; private set; }
        public virtual  Service Service { get; private set; } = null!;

        public static (Appointment? Appointment, string? Error) Create(
            Guid scheduleSlotId,
            string? description,
            Guid userId,
            Guid masterId,
            Guid serviceId,
            MasterSchedule? scheduleSlot,
            User? user,
            Master? master,
            Service? service)
        {
            var errors = new List<string>();

            if (userId == Guid.Empty)
                errors.Add("User ID cannot be empty.");

            if (masterId == Guid.Empty)
                errors.Add("Invalid Master ID.");

            if (serviceId == Guid.Empty)
                errors.Add("Invalid Service ID.");

            if (description != null && description.Length > MAX_DESCRIPTION_LENGTH)
                errors.Add($"Description cannot be Guider than {MAX_DESCRIPTION_LENGTH} characters.");

            if (scheduleSlot == null)
                errors.Add("Schedule slot cannot be null.");

            if (user == null)
                errors.Add("User cannot be null.");
            else if (user.Id != userId)
                errors.Add("Provided userId does not match the user object.");

            if (master == null)
                errors.Add("Master cannot be null.");
            else if (master.Id != masterId)
                errors.Add("Provided masterId does not match the master object.");

            if (service == null)
                errors.Add("Service cannot be null.");
            
            if (errors.Count > 0)
                return (null, string.Join("; ", errors));

            var appointment = new Appointment(scheduleSlotId, description ?? "", userId, masterId, serviceId)
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
                return (false, $"Description cannot be Guider than {MAX_DESCRIPTION_LENGTH} characters.");

            Description = newDescription;
            return (true, null);
        }
    }
}
