namespace Alchemy.Infrastructure.Entities
{
    public class MasterEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Expeirence { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<AppointmentEntity> Appointments { get; set; } = [];
    }
}
