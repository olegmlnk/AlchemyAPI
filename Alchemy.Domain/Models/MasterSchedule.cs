namespace Alchemy.Domain.Models
{
    public class MasterSchedule
    {
        private MasterSchedule() { }

        public long Id { get; private set; }
        public long MasterId { get; private set; }
        public DateTime SlotTime { get; private set; }
        public bool IsBooked { get;  set; }

        public virtual Master? Master { get; private set; }
    }
}
