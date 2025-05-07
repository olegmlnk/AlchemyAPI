namespace AlchemyAPI.Contracts;

public class RegisterUserResponse
{
    public bool IsSuccessful { get; set; }
    public IEnumerable<string>? Errors { get; set; }
}