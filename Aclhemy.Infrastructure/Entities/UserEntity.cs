using Alchemy.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Alchemy.Infrastructure.Entities
{
    public class UserEntity : IdentityUser<Guid> 
    {
        public List<AppointmentEntity>? Appointments { get; set; }
    }
}
