namespace AlchemyAPI.Contracts
{
    public record ServiceResponse
    (
        long Id,
        string Title,
        string Description,
        double Price,
        double DurationInMinutes
    );
}
