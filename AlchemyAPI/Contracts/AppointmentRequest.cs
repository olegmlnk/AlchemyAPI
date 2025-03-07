namespace AlchemyAPI.Contracts
{
    public record AppointmentRequest
    (
        DateTime AppointmentDate,
        string Description
    );
}
