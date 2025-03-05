using Alchemy.Domain.Models;

namespace Alchemy.Infrastructure.Entities
{
    public class MasterEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Expeirence { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Appointment> Appointments { get; set; } = [];
    }
}
