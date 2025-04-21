namespace Alchemy.Infrastructure
{
    public class JwtOptions
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; } 
        public string? SecretKey { get; set; }
        public int ExpiresHours { get; set; }
    }
}
