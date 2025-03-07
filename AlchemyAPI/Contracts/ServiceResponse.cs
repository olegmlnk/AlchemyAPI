namespace AlchemyAPI.Contracts
{
    public record ServiceResponse
    (
        Guid Id,
        string Title,
        string Description,
        decimal Price,
        int Duration
    );
}
