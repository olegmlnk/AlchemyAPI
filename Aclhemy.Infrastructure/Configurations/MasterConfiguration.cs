using Alchemy.Infrastructure.Entities;
using Alchemy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alchemy.Infrastructure.Configurations
{
    public class MasterConfiguration : IEntityTypeConfiguration<MasterEntity>
    {
        public void Configure(EntityTypeBuilder<MasterEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(m => m.Name)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(m => m.Description)
                .HasMaxLength(Master.MAX_DESCRIPTION_LENGTH)
                .IsRequired();

            builder.Property(m => m.Expeirence)
                .HasMaxLength(Master.MAX_EXPEIRENCE_LENGTH)
                .IsRequired();

        }
    }
}
