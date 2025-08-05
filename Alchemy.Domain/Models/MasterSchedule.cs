using Microsoft.Data.SqlClient.DataClassification;

namespace Alchemy.Domain.Models
{
    public class MasterSchedule
    {
        private MasterSchedule() { }

        public Guid Id { get; private set; }
        public Guid MasterId { get; private set; }
        public virtual Master Master { get; private set; } = null!;
        public DateTime SlotTime { get; private set; }
        public bool IsBooked { get; set; }
 
        public virtual Appointment? Appointment { get; private set; }

        public static (MasterSchedule? Schedule, string? Error) Create(Guid id, Guid masterId, DateTime slotTime, Master master)
        {
            var errors = new List<string>();
            
            if (id == Guid.Empty)
                errors.Add("Id cannot be empty.");

            if (master == null)
                errors.Add("Master cannot be null.");
           
            if (masterId == Guid.Empty)
                errors.Add("Invalid masterId");

            if (slotTime < DateTime.UtcNow.AddMinutes(-5))
                errors.Add("Slot time cannot be in the past.");

            if (errors.Any())
                return (null, string.Join("; ", errors));

            var schedule = new MasterSchedule
            {
                Id = id,
                MasterId = masterId,
                SlotTime = slotTime,
                Master = master
            };

            return (schedule, null);
        }

        public (bool Success, string? Error) TryBook(Appointment appointment)
        {
            if (IsBooked)
                return (false, "Slot is already booked.");

            if (appointment == null)
                return (false, "Cannot book with a null appointment.");

            if (appointment.ScheduleSlotId != Id)
                return (false, "Appointment is not for this schedule slot");

            IsBooked = true;
            Appointment = appointment;
            return (true, null);
        }

        public (bool Success, string? Error) TryFreeSlot()
        {
            if (!IsBooked)
                return (false, "Slot is already available");

            IsBooked = false;
            Appointment = null;
            return (true, null);
        }

    }
}
