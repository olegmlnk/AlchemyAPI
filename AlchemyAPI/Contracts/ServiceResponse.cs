namespace AlchemyAPI.Contracts
{
    public record ServiceResponse
    (
        long Id,
        string Title,
        string Description,
        decimal Price,
        int Duration
    );
}
