namespace Alchemy.Domain.Contracts
{
    public record AppointmentRequest
    (
        Guid ScheduleSlotId,
        string Description,
        Guid MasterId,
        Guid ServiceId
    );
}
