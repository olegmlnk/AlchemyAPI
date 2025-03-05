using Alchemy.Infrastructure.Entities;
using Alchemy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alchemy.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(User.MAX_FULLNAME_LENGTH);

            builder.Property(x => x.Email)
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasMaxLength(55);

            builder.Property(x => x.Role)
                .IsRequired();
        }
    }
}
