namespace AlchemyAPI.Contracts
{
    public record RegisterUserRequest
    (
        string Username,
        string Email,
        string Password
    );
}
