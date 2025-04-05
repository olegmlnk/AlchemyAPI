namespace AlchemyAPI.Contracts
{
    public class AppointmentResponse
    {
        public long Id { get; init; }
        public string AppointmentDateFormatted { get; init; } 
        public string Description { get; init; }
        public long UserId { get; init; }
        public long MasterId { get; init; }
        public long ServiceId { get; init; }

        public AppointmentResponse(long id, string appointmentDateFormatted, string description, long userId, long masterId, long serviceId)
        {
            Id = id;
            AppointmentDateFormatted = appointmentDateFormatted;
            Description = description;
            UserId = userId;
            MasterId = masterId;
            ServiceId = serviceId;
        }
    }
}
