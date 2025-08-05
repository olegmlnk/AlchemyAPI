using Alchemy.Domain.Models;

namespace Alchemy.Domain.Contracts
{
    public record MasterRequest
    (
        string Name,
        string Experience,
        string Description
    );
}
