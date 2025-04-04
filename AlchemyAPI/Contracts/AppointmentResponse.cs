using Alchemy.Domain.Models;

namespace AlchemyAPI.Contracts
{
    public record AppointmentResponse
    (
        long Id,
        DateTime AppointmentDate,
        string Description,
        long UserId,
        long MasterId,
        long ServiceId);
}
