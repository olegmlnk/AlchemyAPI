namespace Alchemy.Infrastructure.Entities
{
    public class MasterScheduleEntity
    {
        public long Id { get; set; }
        public long MasterId { get; set; }
        public DateTime SloTime { get; set; }
        public bool IsBooked { get; set; }

        public virtual MasterEntity Master { get; set; } = null!;
        public virtual AppointmentEntity? Appointment { get; set; }
    }
}
