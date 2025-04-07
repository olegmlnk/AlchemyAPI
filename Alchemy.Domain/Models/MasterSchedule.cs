namespace Alchemy.Domain.Models
{
    public class MasterSchedule
    {
        private MasterSchedule() { }

        public long Id { get; set; }
        public long MasterId { get; set; }
        public Master Master { get; set; }
        public DateTime SlotTime { get; set; }
        public bool IsBooked { get;  set; }

        public Appointment Appointment { get; set; }
    }
}
