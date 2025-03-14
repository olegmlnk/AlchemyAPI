using Alchemy.Domain.Models;

namespace AlchemyAPI.Contracts
{
    public record MasterRequest
    (
        string Name,
        string Expeirence,
        string Description,
        List<Appointment> Appointments
    );
}
