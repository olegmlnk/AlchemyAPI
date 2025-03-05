using Alchemy.Domain.Models;

namespace Alchemy.Infrastructure.Entities
{
    public class AppointmentEntity
    {
        public Guid Id { get; set; }
        public DateTime AppointmentDate { get; set; } = DateTime.Now;
        //public Guid ClientId { get; set; }
        //public User? Client {  get; set; }
        //public Guid MasterId { get; set; }
        //public Master? Master { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
