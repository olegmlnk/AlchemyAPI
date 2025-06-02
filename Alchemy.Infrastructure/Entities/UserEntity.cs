using Microsoft.AspNetCore.Identity;

namespace Alchemy.Infrastructure.Entities
{
    public class UserEntity : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ICollection<AppointmentEntity> Appointments { get; set; } = new List<AppointmentEntity>();
    }
}
