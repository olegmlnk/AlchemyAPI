namespace AlchemyAPI.Contracts
{
    public record AppointmentRequest
    (
        DateTime AppointmentDate,
        string Description,
        Guid MasterId,
        Guid UserId,
        Guid ServiceId
    );
}
