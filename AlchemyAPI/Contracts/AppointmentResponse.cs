using Alchemy.Domain.Models;

namespace AlchemyAPI.Contracts
{
    public record AppointmentResponse
    (
        Guid Id,
        DateTime AppointmentDate,
        string Description,
        Guid UserId,
        Guid MasterId,
        Guid ServiceId);
}
