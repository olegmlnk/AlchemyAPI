using Alchemy.Domain.Models;

namespace Alchemy.Infrastructure.Entities
{
    public class MasterScheduleEntity
    {
        public long Id { get; set; }

        public long MasterId { get; set; }
        public MasterEntity? Master { get; set; }

        public DateTime SlotTime { get; set; }
        public bool IsBooked { get; set; }

        public AppointmentEntity? Appointment { get; set; }
    }
}
