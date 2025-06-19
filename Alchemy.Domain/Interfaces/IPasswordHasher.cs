namespace Alchemy.Domain.Interfaces
{
    public interface IPasswordHasher
    {
        string GenerateHash(string password);
        bool Verify(string password, string hashedPassword);
    }
}