using Alchemy.Domain.Models;

namespace Alchemy.Infrastructure.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? Email { get; set; }
        public List<Appointment>? Appointments { get; set; }
    }
}
