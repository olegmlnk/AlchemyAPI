namespace AlchemyAPI.Contracts
{
    public record AppointmentRequest
    (
        DateTime AppointmentDate,
        //Guid ClientId,
        //User? Client,
        //Guid MasterId,
        //Master? Master,
        string Description
    );
}
