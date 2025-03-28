namespace AlchemyAPI.Contracts
{
    public record LoginUserRequest
    (
        string Username,
        string Email,
        string Password
    );
}
