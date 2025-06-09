using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using Alchemy.Domain.Models;

namespace Alchemy.Infrastructure.Entities
{
    public class AppointmentEntity
    {
     public long Id { get; set; }
     public string Description { get; set; }

     public string UserId { get; set; } = string.Empty;
     public long MasterId { get; set; }
     public long ServiceId { get; set; }
     public long ScheduleSlotId { get; set; }

     public virtual UserEntity User { get; set; } = null!;
     public virtual MasterEntity Master { get; set; } = null!;
     public virtual ServiceEntity Service { get; set; } = null!;
     public virtual MasterScheduleEntity ScheduleSlot { get; set; } = null!;
    }
}
