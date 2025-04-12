namespace AlchemyAPI.Contracts
{
    public record ServiceRequest
    (
        string Title,
        string Description,
        double Price,
        int Duration
    );
}
