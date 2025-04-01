namespace Alchemy.Domain.Models
{
    public class MasterSchedule
    {
        private MasterSchedule() { }

        public Guid Id { get; private set; }
        public Guid MasterId { get; private set; }
        public DateTime AvailableFrom { get; private set; }
        public DateTime AvailableTo { get; private set; }
        public bool IsBooked { get;  set; }

        public virtual Master? Master { get; private set; }
    }
}
