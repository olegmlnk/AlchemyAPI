using Alchemy.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Alchemy.Infrastructure.Entities
{
    public class UserEntity : IdentityUser<long> 
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty; 
        public ICollection<AppointmentEntity>? Appointments { get; set; }
    }
}
