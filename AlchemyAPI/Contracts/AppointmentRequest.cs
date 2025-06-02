namespace AlchemyAPI.Contracts
{
    public record AppointmentRequest
    (
        long ScheduleSlotId,
        string Description,
        long MasterId,
        string UserId,
        long ServiceId
    );
}
