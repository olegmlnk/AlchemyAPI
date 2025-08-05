namespace Alchemy.Domain.Contracts
{
    public record AppointmentResponse
    (
       Guid Id,
       Guid ScheduleSlotId,
       string? Description,
       string UserId,
       Guid MasterId,
       Guid ServiceId
    );
}
