using Alchemy.Domain.Models;

namespace AlchemyAPI.Contracts
{
    public record AppointmentResponse
    (
        Guid Id,
        DateTime AppointmentDate,
        //Guid ClientId,
        //User? Client,
        //Guid MasterId,
        //Master? Master,
        string Description);
}
