namespace AlchemyAPI.Contracts
{
    public record AppointmentRequest
    (
        long ScheduleSlotId,
        string Description,
        long MasterId,
        long UserId,
        long ServiceId
    );
}
