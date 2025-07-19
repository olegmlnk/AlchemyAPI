using Microsoft.Data.SqlClient.DataClassification;

namespace Alchemy.Domain.Models
{
    public class MasterSchedule
    {
        protected MasterSchedule() { }
        private MasterSchedule(long masterId, DateTime slotTime, Master master)
        {
            MasterId = masterId;
            SlotTime = slotTime;
            IsBooked = false;
            Master = master;
        }

        public long Id { get; private set; }
        public long MasterId { get; private set; }
        public virtual Master Master { get; private set; } = null!;
        public DateTime SlotTime { get; private set; }
        public bool IsBooked { get; set; }
 
        public virtual Appointment? Appointment { get; private set; }

        public static (MasterSchedule? Schedule, string? Error) Create(long masterId, DateTime slotTime, Master master)
        {
            var errors = new List<string>();

            if (master == null)
                errors.Add("Master cannot be null.");
            else if (master.Id != masterId)
                errors.Add("Provided masterId does match the master object.");

            if (masterId <= 0)
                errors.Add("Invalid masterId");

            if (slotTime < DateTime.UtcNow.AddMinutes(-5))
                errors.Add("Slot time cannot be in the past.");

            if (errors.Any())
                return (null, string.Join("; ", errors));

            var schedule = new MasterSchedule(masterId, slotTime, master);

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
