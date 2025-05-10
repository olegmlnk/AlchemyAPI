namespace AlchemyAPI.Contracts;

public class ExternalLoginRequest
{
    public string? Provider { get; set; }
    public string? IdToken { get; set; }
}