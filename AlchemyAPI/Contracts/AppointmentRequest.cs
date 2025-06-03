namespace AlchemyAPI.Contracts
{
    public record AppointmentRequest
    (
        long ScheduleSlotId,
        string Description,
        string UserId,
        long MasterId,
        long ServiceId
    );
}
