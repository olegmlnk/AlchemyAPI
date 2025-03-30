
namespace Alchemy.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid> DeleteUser(Guid id);
        Task<Guid> GetUser(Guid id);
    }
}