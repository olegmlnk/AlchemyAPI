using Alchemy.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alchemy.Infrastructure.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.Property(r => r.NormalizedName)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.HasIndex(r => r.NormalizedName)
                   .IsUnique();

            builder.Property(r => r.ConcurrencyStamp)
                   .IsConcurrencyToken();
        }
    }
}
