using Alchemy.Domain.Models;
using Alchemy.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alchemy.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users");

            builder.Property(u => u.FirstName)
                .HasMaxLength(User.MAX_NAME_LENGTH)
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasMaxLength(User.MAX_NAME_LENGTH)
                .IsRequired();

            builder.HasMany(u => u.Appointments)
                .WithOne(u => u.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
