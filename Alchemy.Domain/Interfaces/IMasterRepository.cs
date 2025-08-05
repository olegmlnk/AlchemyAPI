using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IMasterRepository : IGenericRepository<Master>
    {
        // Additional methods specific to Master can be defined here if needed
        // For example, you might want to add methods for specific queries or operations
        // that are unique to the Master entity.
    }
}