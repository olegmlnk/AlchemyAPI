namespace AlchemyAPI.Contracts
{
    public record AppointmentResponse
    (
       long Id,
       long ScheduleSlotId,
       string? Description,
       string UserId,
       long MasterId,
       long ServiceId
    );
}
