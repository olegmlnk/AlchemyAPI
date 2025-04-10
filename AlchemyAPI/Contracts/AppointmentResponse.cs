namespace AlchemyAPI.Contracts
{
    public class AppointmentResponse
    {
        public long Id { get; init; }
        public long ScheduleSlotId { get; init; } 
        public string? Description { get; init; }
        public long UserId { get; init; }
        public long MasterId { get; init; }
        public long ServiceId { get; init; }
    }
}
