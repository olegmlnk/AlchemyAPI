namespace Alchemy.Infrastructure.Entities
{
    public class AppointmentEntity
    {
        public long Id { get; set; }
        public DateTime AppointmentDate { get; set; } = DateTime.Now;
        public string Description { get; set; } = string.Empty;
        public long UserId { get; set; }
        public UserEntity? User { get; set; }
        public long ServiceId { get; set; }
        public ServiceEntity? Service { get; set; }
        public long MasterId { get; set; }
        public MasterEntity? Master { get; set; }
    }
}
