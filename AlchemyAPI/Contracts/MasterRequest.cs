using Alchemy.Domain.Models;

namespace AlchemyAPI.Contracts
{
    public record MasterRequest
    (
        string Name,
        string Experience,
        string Description
    );
}
