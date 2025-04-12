public class MasterSchedule
{
    public long Id { get; set; }
    public long MasterId { get; set; }
    public Master Master { get; set; }
    public DateTime SlotTime { get; set; }
    public bool IsBooked { get; set; }
    public Appointment Appointment { get; set; }

    // Add a public parameterless constructor
    public MasterSchedule() { }
}
