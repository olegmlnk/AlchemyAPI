
namespace Alchemy.Domain.Interfaces
{
    public interface IUserService
    {
        Task<string> Login(string email, string password);
        Task Register(string username, string email, string password);
    }
}