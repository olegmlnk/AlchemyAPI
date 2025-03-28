namespace Alchemy.Domain
{
    public interface IPasswordHasher
    {
        string GenerateHash(string password);
    }
}