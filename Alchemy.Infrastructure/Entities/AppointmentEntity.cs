﻿using System.ComponentModel.DataAnnotations;

namespace Alchemy.Infrastructure.Entities
{
    public class AppointmentEntity
    {
        public long Id { get; set; }
        public long ScheduleSlotId { get;  set; }
        public MasterScheduleEntity ScheduleSlot { get; set; }
        public string Description { get; set; } = string.Empty;
        public string UserId { get; set; }
        public UserEntity User { get; set; }
        public long ServiceId { get; set; }
        public ServiceEntity Service { get; set; }
        public long MasterId { get; set; }
        public MasterEntity Master { get; set; }
    }
}
