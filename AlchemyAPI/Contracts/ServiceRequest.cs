namespace AlchemyAPI.Contracts
{
    public record ServiceRequest
    (
        string Title,
        string Description,
        decimal Price,
        int Duration
    );
}
