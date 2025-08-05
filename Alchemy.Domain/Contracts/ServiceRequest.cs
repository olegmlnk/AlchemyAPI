namespace Alchemy.Domain.Contracts
{
    public record ServiceRequest
    (
        string Title,
        string Description,
        double Price,
        double DurationInMinutes
    );
}
