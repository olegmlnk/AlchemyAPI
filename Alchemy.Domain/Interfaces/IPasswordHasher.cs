namespace Alchemy.Domain
{
    public interface IPasswordHasher
    {
        string GenerateHash(string password);
        bool Verify(string password, string hashedPassword);
    }
}