namespace Alchemy.Domain.Contracts
{
    public record ServiceResponse
    (
        Guid Id,
        string Title,
        string Description,
        double Price,
        double DurationInMinutes
    );
}
