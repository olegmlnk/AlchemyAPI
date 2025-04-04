namespace AlchemyAPI.Contracts
{
    public record AppointmentRequest
    (
        DateTime AppointmentDate,
        string Description,
        long MasterId,
        long UserId,
        long ServiceId
    );
}
