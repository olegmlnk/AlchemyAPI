using System.Diagnostics;

namespace Alchemy.Infrastructure.Entities
{
    public class MasterEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<AppointmentEntity> Appointments { get; set; } = new List<AppointmentEntity>();
        public virtual ICollection<MasterScheduleEntity> ScheduleSlots { get; set; } = new List<MasterScheduleEntity>();
    }
}
