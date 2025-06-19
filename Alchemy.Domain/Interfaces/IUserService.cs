
namespace Alchemy.Domain.Interfaces
{
    public interface IUserService
    {
        Task<(bool Success, IEnumerable<string> Errors)> Register(string username, string email, string password,  string firstName, string lastName);
        Task<(string? Token, string? Error)> Login(string email, string password);
    }
}