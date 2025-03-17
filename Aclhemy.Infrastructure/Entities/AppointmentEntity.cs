using Alchemy.Domain.Models;
namespace Alchemy.Infrastructure.Entities
{
    public class AppointmentEntity
    {
        public Guid Id { get; set; }
        public DateTime AppointmentDate { get; set; } = DateTime.Now;
        public string Description { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid ServiceId { get; set; }
        public Service? Service { get; set; }
        public Guid MasterId { get; set; }
        public MasterEntity? Master { get; set; }
    }
}
