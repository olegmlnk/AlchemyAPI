using Alchemy.Infrastructure.Entities;

namespace AlchemyAPI.Contracts
{
    public record MasterRepsonse
    (
        long Id,
        string Name,
        string Experience,
        string Description
    );
}
