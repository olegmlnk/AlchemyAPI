namespace Alchemy.Domain.Models
{
    public class MasterSchedule
    {
        public Guid Id { get; set; }
        public Guid MasterId { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public bool IsBooked { get; set; }

        public virtual Master? Master { get; set; }
    }
}
